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
            if (id != null)
            {
                var user = await _userManager.FindByIdAsync(id);
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser>> Login([FromBody] UserForm userForm)
        {
            var user = await _userManager.FindByNameAsync(userForm.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, userForm.Password))
            {
                var token = _jwtHandler.GenerateTokenOptions(user);
                return Ok(new AuthResponse { Token = token });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserForm userForm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = userForm.Username };
            var result = await _userManager.CreateAsync(user, userForm.Password);

            if (!result.Succeeded) return BadRequest(result);

            var token = _jwtHandler.GenerateTokenOptions(user);
            return Ok(new AuthResponse { Token = token });
        }

        [Authorize]
        [HttpGet("refresh")]
        public async Task<ActionResult> RefreshToken()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var token = _jwtHandler.GenerateTokenOptions(user);
                    return Ok(new AuthResponse { Token = token });
                }
            }
            return Unauthorized();
        }
    }
}
