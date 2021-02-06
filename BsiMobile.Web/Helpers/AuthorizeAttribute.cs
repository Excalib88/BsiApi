using System;
using BsiMobile.Web.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BsiMobile.Web.Helpers
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var user = (User) context.HttpContext.Items["User"];

			if (user == null)
				// not logged in
				context.Result = new JsonResult(new {message = "Unauthorized"})
				{
					StatusCode = StatusCodes.Status401Unauthorized
				};
		}
	}
}