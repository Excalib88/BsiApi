using System;

namespace BsiMobile.Web.Domain.Services.Users
{
	public class UserModel
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string Username { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}