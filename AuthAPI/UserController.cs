using AuthAPI.Services;
using AuthAPI.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegistrationModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _userService.RegisterUser(user);
                    if (result.Success)
                        return Ok(result);
                    else
                        return BadRequest(result);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500,
                    new
                    { Message = "Sorry, an error occurred while trying to register the user." });
            }
        }
    }
}
