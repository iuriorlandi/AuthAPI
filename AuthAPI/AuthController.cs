using AuthAPI.Services;
using AuthAPI.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _authService.Login(loginModel);
                    if (result.Success)
                        return Ok(result);

                    return BadRequest(result);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500,
                    new
                    { Message = "Sorry, an error occurred while trying to login." });
            }
        }

        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
                if (string.IsNullOrWhiteSpace(token))
                    return BadRequest("Token not provided");

                var result = _authService.ValidateToken(token);

                if (result.Success)
                    return Ok(result);

                return BadRequest(string.Join(' ', result.Errors));

            }
            catch (Exception)
            {
                return StatusCode(500, $"Token validation failed.");
            }
        }
    }
}
