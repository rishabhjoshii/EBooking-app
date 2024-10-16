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

        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userExisted = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == registerDto.Email || user.UserName == registerDto.Username || user.PhoneNumber == registerDto.PhoneNumber);
                if (userExisted != null)
                {
                    return BadRequest("Username/Email/PhoneNumber already registered");
                }
                
                var newUser = new ApplicationUser{
                    UserName = registerDto.Username.ToLower(),
                    Email = registerDto.Email.ToLower(),
                    PhoneNumber = registerDto.PhoneNumber,
                };

                var createdUser = await _userManager.CreateAsync(newUser, registerDto.Password);

                if(createdUser.Succeeded){
                     return Ok(
                        new NewUserDto{
                            Message = "User created successfully",
                            UserName = newUser.UserName,
                            Email = newUser.Email,
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

            return Ok(
                new NewUserDto{
                    Message = "login successful",
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
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
                PhoneNumber = appUser.PhoneNumber
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(){
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
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
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
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
            if (updateUserDto.Email != null && updateUserDto.Email != "") 
                user.Email = updateUserDto.Email;
            if (updateUserDto.PhoneNumber != null && updateUserDto.PhoneNumber != "") 
                user.PhoneNumber = updateUserDto.PhoneNumber;
            if (updateUserDto.UserName != null && updateUserDto.UserName != "")
                user.UserName = updateUserDto.UserName;

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
            
            return Ok(
                new NewUserDto{
                    Message = "profile updated successfully",
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}