using System.Threading.Tasks;
using BsiMobile.Web.Domain.Services.Users;
using BsiMobile.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BsiMobile.Web.Controllers
{
	[ApiController]
	[Route("user")]
	public class UsersController : ControllerBase
	{
		[HttpPost("authenticate")]
		public IActionResult Authenticate(AuthenticateRequest model)
		{
			var response = _userService.Authenticate(model);

			if (response == null)
				return BadRequest(new {message = "Username or password is incorrect"});

			return Ok(response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(UserModel userModel)
		{
			var response = await _userService.Register(userModel);

			if (response == null) return BadRequest(new {message = "Didn't register!"});

			return Ok(response);
		}

		[Authorize]
		[HttpGet("info")]
		public IActionResult GetInfo()
		{
			var username = ""; //_currentUser.Username;

			var info = _userService.GetInfo(username);

			if (info == null) return BadRequest("User was not found");

			return Ok(info);
		}

		#region DI, ctor

		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		#endregion
	}
}