using BsiMobile.Web.DataAccess.Entities;

namespace BsiMobile.Web.Domain.Services.Users
{
	public class AuthenticateResponse
	{
		public AuthenticateResponse(User user)
		{
			Id = user.Id;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Patronymic = user.Patronymic;
			Username = user.Username;
			Email = user.Email;
		}

		public AuthenticateResponse(User user, string token) : this(user)
		{
			Token = token;
		}

		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}
}