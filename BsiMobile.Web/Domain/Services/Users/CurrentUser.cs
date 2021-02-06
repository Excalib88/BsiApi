using System.Linq;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.Helpers;
using Microsoft.AspNetCore.Http;

namespace BsiMobile.Web.Domain.Services.Users
{
	public class CurrentUser : User, ICurrentUser
	{
		public CurrentUser(IHttpContextAccessor httpContextAccessor)
		{
			if (httpContextAccessor.HttpContext != null)
			{
				var currentUser = (User) httpContextAccessor.HttpContext.Items
					.FirstOrDefault(x => (string) x.Key == "User").Value;

				if (currentUser == null) throw new ClientErrorException("User not found");

				Id = currentUser.Id;
				Username = currentUser.Username;
				FirstName = currentUser.FirstName;
				LastName = currentUser.LastName;
				Patronymic = currentUser.Patronymic;
				Email = currentUser.Email;
				BirthDate = currentUser.BirthDate;
			}
		}
	}
}