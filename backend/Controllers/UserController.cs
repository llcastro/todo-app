using System.Threading.Tasks;
using backend.Dto;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateUser([FromBody] UserDto userDto)
    {
      var user = await _userService.Authenticate(userDto);

      if (user == null)
      {
        return BadRequest(new { message = "User not found" });
      }

      return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
      var user = await _userService.CreateUser(userDto);

      if (user == null)
      {
        return BadRequest(new { message = "Failed to create User" });
      }

      return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var users = await _userService.GetAll();

      return Ok(users);
    }
  }
}