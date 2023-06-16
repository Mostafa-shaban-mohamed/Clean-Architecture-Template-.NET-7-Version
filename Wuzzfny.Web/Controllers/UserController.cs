using Microsoft.AspNetCore.Mvc;
using Wuzzfny.Application.Users;
using Wuzzfny.Application.Users.Dtos;

namespace Wuzzfny.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersWithoutFiltering()
        {
            var result = await _userService.GetAllForSelect();
            return StatusCode((int)result.httpStatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(UserDto model)
        {
            var result = await _userService.AddUser(model);
            return StatusCode((int)result.httpStatusCode, result);
        }
    }
}