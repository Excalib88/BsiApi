using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BsiMobile.Web.Helpers
{
	public class AuthOptions
	{
		public const string Issuer = "MyAuthServer"; // издатель токена
		public const string Audience = "MyAuthClient"; // потребитель токена
		private const string Key = "secretkeyclient0"; // ключ для шифрации
		public const int Lifetime = 3600; // время жизни токена

		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new(Encoding.ASCII.GetBytes(Key));
		}
	}
}