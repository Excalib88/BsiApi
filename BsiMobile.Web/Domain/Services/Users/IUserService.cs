using System.Threading.Tasks;
using BsiMobile.Web.DataAccess.Entities;

namespace BsiMobile.Web.Domain.Services.Users
{
	public interface IUserService
	{
		AuthenticateResponse GetInfo(string username);
		AuthenticateResponse Authenticate(AuthenticateRequest model);
		AesKeyModel GetAesKey(long userId); 
		Task<AuthenticateResponse> Register(UserModel userModel);

		User GetById(int id);
	}
}