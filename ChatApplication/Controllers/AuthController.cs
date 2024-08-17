using ChatApplication.DTOs;
using ChatApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;

        public AuthController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await userService.RegisterUserAsync(dto.Username, dto.Password);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await userService.AuthenticateUserAsync(dto.Username, dto.Password);
            if (user == null)
                return Unauthorized();

            // Generate JWT token here

            return Ok(new { Token = "jwt_token" });
        }
    }
}
