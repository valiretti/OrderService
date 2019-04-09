using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using OrderService.Logic.Services;
using OrderService.Model;

namespace OrderService.Website.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var id = await _userService.Register(model);

            return Ok($"\"{id}\"");
        }

        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword([FromQuery] string password)
        {
            await _userService.ChangePassword(User.GetSubjectId(), password);
            return NoContent();
        }

        [HttpPut("profile")]
        public async Task<IActionResult> ChangeProfile([FromBody] UpdateProfileModel model)
        {
            if (model == null) return BadRequest();
            model.UserId = User.GetSubjectId();

            await _userService.ChangeProfile(model);
            return NoContent();
        }
    }
}