using System.Threading.Tasks;
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

            return Ok(id);
        }
    }
}