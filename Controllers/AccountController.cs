using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Account;
using WebApi.Interfaces;
using WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            return await RegisterUser(model, "User");
        }

        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] RegisterDto model)
        {
            return await RegisterUser(model, "Admin");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.UserName) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Username and/or Password not specified");
            }

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
            {
                var token = _tokenService.CreateToken(user);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }

        private async Task<IActionResult> RegisterUser(RegisterDto model, string role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, model.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, role);

                    if (roleResult.Succeeded)
                    {
                        var token = _tokenService.CreateToken(appUser);
                        return Ok(new { message = $"{role} registered successfully", token });
                    }
                    else
                    {
                        return StatusCode(500, new { message = $"An error occurred while assigning the {role} role to the user" });
                    }
                }
                else
                {
                    return StatusCode(500, new { message = "An error occurred while creating the user" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}
