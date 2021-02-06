using System.Collections.Generic;
using BsiMobile.Web.Helpers;
using JsonApiSerializer.JsonApi;
using Microsoft.AspNetCore.Mvc;

namespace BsiMobile.Web.Controllers
{
	[ApiController]
	[Authorize]
	[Produces("application/json")]
	public class BaseController : Controller
	{
		protected IActionResult BadRequest(string errorDetail)
		{
			return StatusCode(400, new DocumentRoot<object>
			{
				Errors = new List<Error>
				{
					new() {Detail = errorDetail}
				}
			});
		}
	}
}