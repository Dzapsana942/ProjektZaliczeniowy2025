using Microsoft.AspNetCore.Mvc;
using ProjektZaliczeniowy2.Services;

namespace ProjektZaliczeniowy2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController()
        {
            _jwtService = new JwtService();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            if (request.Email == "test@test.com" && request.Name == "testuser")
            {
                var token = _jwtService.GenerateToken(request.Name, request.Email);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
