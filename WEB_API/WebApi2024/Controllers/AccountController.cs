using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2024.Dtos.Account;
using WebApi2024.Interfaces;
using WebApi2024.Models;

namespace WebApi2024.Controllers
{
    [Route("api/account")]
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

        [HttpPatch("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerData)
        {
            try
            {
                if (!ModelState.IsValid) 
                { 
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser 
                { 
                    UserName = registerData.UserName, 
                    Email = registerData.Email 
                };
                var createUser = await _userManager.CreateAsync(appUser, registerData.Password);
                if(createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        //return Ok("User Created");
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                            );

                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors.ToString());
                    }
                }else
                {
                    return StatusCode(500,createUser.Errors.ToString());    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginData)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginData.UserName.ToLower());

            if(user == null) return Unauthorized("Invalid User Name!");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("User Name not found or password incorrect !");
            }
            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
        }
    }
}
