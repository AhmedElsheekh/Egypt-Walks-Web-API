using EgyptWalks.API.DTOs;
using EgyptWalks.Core.Models.Identity;
using EgyptWalks.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
   
        public AuthController(UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);

            if(result.Succeeded)
            {
                if(registerDto.Roles.Any())
                {
                    var roleResult = await _userManager.AddToRolesAsync(applicationUser, registerDto.Roles);
                    if (roleResult.Succeeded)
                        return Ok("User registered successfully, please login!");
                    return BadRequest("Error with adding roles");
                }
            }
            return BadRequest("Error while registering the user");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) 
                return BadRequest("Email is not found");

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkPassword)
                return BadRequest("Invalid Password");

            //Create Token
            var roles = await _userManager.GetRolesAsync(user);

            if (roles is null)
                return BadRequest("User has no roles, contact your admin");

            var jwtToken = _tokenService.CreateJWTToken(user, roles.ToList());
            var loginResponse = new LoginResponseDto
            {
                JwtToken = jwtToken
            };
            return Ok(loginResponse);

        }
    }
}
