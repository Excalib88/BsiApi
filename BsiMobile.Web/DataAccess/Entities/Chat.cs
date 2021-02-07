using System.Collections.Generic;

namespace BsiMobile.Web.DataAccess.Entities
{
	public class Chat : BaseEntity
	{
		public List<ChatUsers> ChatUsers { get; set; }
		public List<Message> Messages { get; set; }
	}
}