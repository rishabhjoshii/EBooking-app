using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Dtos.User;
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
                            Token = _tokenService.CreateToken(newUser)
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
    }
}