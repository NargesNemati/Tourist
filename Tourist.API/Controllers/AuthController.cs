using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tourist.API.Models.Domain;
using Tourist.API.Models.DTO;
using Tourist.API.Repositories;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new ApplicationUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
                FullName = registerRequestDto.FullName,
                BirthDay = registerRequestDto.BirthDay,
            };

            // مرحله اول: ساخت کاربر
            var createResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest(createResult.Errors);
            }

            // مرحله دوم: اختصاص نقش‌ها
            IdentityResult roleResult;

            if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                roleResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
            }
            else
            {
                
                roleResult = await userManager.AddToRoleAsync(identityUser, "User");
            }

            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(identityUser);
                return BadRequest(roleResult.Errors);
            }

            return Ok("User was registered. Please login");
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var chechPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if(chechPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
