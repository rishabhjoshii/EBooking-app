using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Dtos.User;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IImageRepository _imageRepo;

        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, IImageRepository imageRepo) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _imageRepo = imageRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userExisted = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == registerDto.Email || user.UserName == registerDto.Username);
                if (userExisted != null)
                {
                    return BadRequest("Username/Email already registered");
                }
                
                var newUser = new ApplicationUser{
                    UserName = registerDto.Username.ToLower(),
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email.ToLower(),
                    PhoneNumber = registerDto.PhoneNumber,
                    PreferredCurrency = registerDto.PreferredCurrency,
                    PreferredLanguage = registerDto.PreferredLanguage,
                };

                var createdUser = await _userManager.CreateAsync(newUser, registerDto.Password);

                if(createdUser.Succeeded){
                     return Ok(
                        new NewUserDto{
                            Message = "User created successfully",
                            UserName = newUser.UserName,
                            FirstName = newUser.FirstName,
                            LastName = newUser.LastName,
                            Email = newUser.Email,
                            PhoneNumber = newUser.PhoneNumber,
                            PreferredCurrency = newUser.PreferredCurrency,
                            PreferredLanguage = newUser.PreferredLanguage,
                            Token = "please login",
                        }
                    );
                }
                else{
                    return StatusCode(500, createdUser.Errors); 
                }

            }
            catch(Exception e)
            {
                return StatusCode(500, new { Message = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail.ToLower());
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail.ToLower());
                if (user == null)
                {
                    return BadRequest("User not found. Invalid username or Email address");
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)
            {
                return Unauthorized("Password you entered is incorrect.");

            }

            var userProfileImage = await _imageRepo.GetUserProfileImageByUserId(user.Id);
            string profileImageUrl = userProfileImage?.FilePath; 

            return Ok(
                new NewUserDto{
                    Message = "login successful",
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    PreferredCurrency = user.PreferredCurrency,
                    PreferredLanguage = user.PreferredLanguage,
                    Token = _tokenService.CreateToken(user),
                    ProfileImageUrl = profileImageUrl,
                }
            );
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();
            return Ok("User Logged out Successfully");
        }

        [Authorize]
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return Unauthorized("User not found");
            }
            
            return Ok(new UserInfoDto{
                message = "user info fetched successfully",
                UserName = appUser.UserName,
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                PhoneNumber = appUser.PhoneNumber,
                PreferredCurrency = appUser.PreferredCurrency,
                PreferredLanguage = appUser.PreferredLanguage,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.Include(u => u.ProfileImage).ToListAsync();
            var userDtos = users.Select(user => new GetUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PreferredCurrency = user.PreferredCurrency,
                PreferredLanguage = user.PreferredLanguage,
                ProfileImageUrl = user.ProfileImage?.FilePath
            }).ToList();

            return Ok(userDtos);
        }


        // [HttpDelete]
        // public async Task<IActionResult> DeleteUser(){
        //     var users = await _userManager.Users.ToListAsync();

        //     foreach (var user in users){
        //         await _userManager.DeleteAsync(user);
        //     }

        //     return Ok();
        // }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto updateUserDto)
        {
            ValidateFileUpload(updateUserDto.ProfileImage);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //get the user from the claims
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            //update the user details
            if (!string.IsNullOrEmpty(updateUserDto.FirstName))
                user.FirstName = updateUserDto.FirstName;
            if (!string.IsNullOrEmpty(updateUserDto.LastName))
                user.LastName = updateUserDto.LastName;
            if (!string.IsNullOrEmpty(updateUserDto.PhoneNumber))
                user.PhoneNumber = updateUserDto.PhoneNumber;
            if (!string.IsNullOrEmpty(updateUserDto.UserName))
                user.UserName = updateUserDto.UserName.ToLower();
            if (!string.IsNullOrEmpty(updateUserDto.PreferredCurrency))
                user.PreferredCurrency = updateUserDto.PreferredCurrency;
            if (!string.IsNullOrEmpty(updateUserDto.PreferredLanguage))
                user.PreferredLanguage = updateUserDto.PreferredLanguage;

            
            //update the user
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            //password updates
            if (updateUserDto.OldPassWord != null && updateUserDto.OldPassWord != "" && updateUserDto.NewPassWord != null && updateUserDto.NewPassWord != "")
            {
                var userUPDATED = await _userManager.ChangePasswordAsync(user, updateUserDto.OldPassWord, updateUserDto.NewPassWord);
                if (!userUPDATED.Succeeded)
                {
                    return StatusCode(500, userUPDATED.Errors);
                }
            }

            string profileImageUrl = null;
            // Handle profile image update
            if (updateUserDto.ProfileImage != null && updateUserDto.ProfileImage.Length > 0)
            {
                // Check if a profile image already exists for the user
                var existingProfileImage = await _imageRepo.GetUserProfileImageByUserId(user.Id);
                if (existingProfileImage != null)
                {
                    //Delete the existing file if necessary
                    // var oldFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", existingProfileImage.FileName);
                    // if (System.IO.File.Exists(oldFilePath))
                    // {
                    //     System.IO.File.Delete(oldFilePath);
                    // }

                    // Update the existing profile image details
                    existingProfileImage.File = updateUserDto.ProfileImage;
                    existingProfileImage.FileExtension = Path.GetExtension(updateUserDto.ProfileImage.FileName);
                    existingProfileImage.FileSizeInBytes = updateUserDto.ProfileImage.Length;
                    existingProfileImage.FileName = $"{Guid.NewGuid()}{Path.GetExtension(updateUserDto.ProfileImage.FileName)}";  // Generating new GUID filename

                    Console.WriteLine("look hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee, control reaching here");
                    var updatedProfileImage = await _imageRepo.UpdateUserProfileImage(existingProfileImage);
                    profileImageUrl = updatedProfileImage.FilePath;
                }
                else
                {
                    // Create new profile image entry if none exists
                    var newProfileImage = new UserProfileImage
                    {
                        File = updateUserDto.ProfileImage,
                        FileExtension = Path.GetExtension(updateUserDto.ProfileImage.FileName),
                        FileSizeInBytes = updateUserDto.ProfileImage.Length,
                        FileName = $"{Guid.NewGuid()}{Path.GetExtension(updateUserDto.ProfileImage.FileName)}",  // Generating GUID filename
                        ApplicationUserId = user.Id,
                    };

                    var updatedProfileImage = await _imageRepo.UploadUserProfileImage(newProfileImage);
                    profileImageUrl = updatedProfileImage.FilePath;
                }
            }
            
            return Ok(
                new NewUserDto{
                    Message = "profile updated successfully",
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    PreferredCurrency = user.PreferredCurrency,
                    PreferredLanguage = user.PreferredLanguage,
                    Token = _tokenService.CreateToken(user),
                    ProfileImageUrl = user.ProfileImage?.FilePath,
                    //ProfileImageUrl = profileImageUrl,
                }
            );
        }

        // function to validate image upload
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png"};

            try{
                if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ModelState.AddModelError("file", $"Unsupported file extension: {file.FileName}");
                }

                if (file.Length > 2097152)
                {
                    ModelState.AddModelError("file", $"File size exceeds 2MB: {file.FileName}");
                }
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}