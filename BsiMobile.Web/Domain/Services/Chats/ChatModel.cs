using System.Collections.Generic;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.Domain.Services.Messages;
using BsiMobile.Web.Domain.Services.Users;

namespace BsiMobile.Web.Domain.Services.Chats
{
	public class ChatModel
	{
		public long Id { get; set; }
		public List<long> ChatUsers { get; set; }
		public List<UserModel> Users { get; set; }
		
		public List<MessageModel> Messages { get; set; }
	}
}