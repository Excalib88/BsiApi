using System.IO;
using System.Security.Cryptography;
using BsiMobile.Web.Domain.Services.Users;

namespace BsiMobile.Web.Helpers
{
	public class CryptHelper
	{
		public static byte[] Encrypt(string text, AesKeyModel aesKeyModel)
		{
			using var rijndelManaged = new RijndaelManaged
			{
				Key = aesKeyModel.Key, 
				IV = aesKeyModel.Iv
			};
			
			var encryptor = rijndelManaged.CreateEncryptor(rijndelManaged.Key, rijndelManaged.IV);

			using var ms = new MemoryStream();
			using var csEncrypt = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
			using (var swEncrypt = new StreamWriter(csEncrypt))
			{
				swEncrypt.Write(text);
			}
					
			return ms.ToArray();
		}

		public static string Decrypt(byte[] encryptedText, AesKeyModel aesKeyModel)
		{
			using var rijndelManaged = new RijndaelManaged
			{
				Key = aesKeyModel.Key, 
				IV = aesKeyModel.Iv
			};

			var decryptor = rijndelManaged.CreateDecryptor(rijndelManaged.Key, rijndelManaged.IV);

			using var msDecrypt = new MemoryStream(encryptedText);
			using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
			using var srDecrypt = new StreamReader(csDecrypt);
			
			return srDecrypt.ReadToEnd();
		}

		public static AesKeyModel GenerateAesKeys()
		{
			using var rijndaelManaged = new RijndaelManaged();

			return new AesKeyModel
			{
				Key = rijndaelManaged.Key,
				Iv = rijndaelManaged.IV
			};
		}
	}
}