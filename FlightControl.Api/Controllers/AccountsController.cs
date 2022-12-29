using FlightControl.Api.Models;
using FlightControl.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        public AccountsController(UserManager<ApplicationUser> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok("");
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult> GetUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser>> Login([FromBody] UserForm userForm)
        {
            var user = await _userManager.FindByNameAsync(userForm.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForm.Password))
            {
                return Unauthorized(new AuthResponse { ErrorMessage = "Invalid Authentication" });
            }
            var token = _jwtHandler.GenerateTokenOptions(user);
            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserForm userForm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = userForm.Username };
            var result = await _userManager.CreateAsync(user, userForm.Password);

            if (!result.Succeeded) return BadRequest(result);

            var token = _jwtHandler.GenerateTokenOptions(user);
            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token });
        }

        [Authorize]
        [HttpGet("refresh")]
        public async Task<ActionResult> RefreshToken()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Unauthorized(new AuthResponse { ErrorMessage = "Invalid Authentication" });
            }

            var token = _jwtHandler.GenerateTokenOptions(user);
            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token });
        }
    }
}
