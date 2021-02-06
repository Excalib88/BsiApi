using System;

namespace BsiMobile.Web.Helpers
{
	public class ClientErrorException : Exception
	{
		public ClientErrorException(string message) : base(message)
		{
		}
	}
}