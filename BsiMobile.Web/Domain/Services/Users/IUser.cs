namespace BsiMobile.Web.Domain.Services.Users
{
	public interface IUser
	{
		long Id { get; set; }
		string Username { get; set; }
	}
}