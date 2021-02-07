using System.Collections.Generic;
using System.Threading.Tasks;
using BsiMobile.Web.DataAccess.Entities;

namespace BsiMobile.Web.Domain.Services.Messages
{
	public interface IMessagesService
	{
		Task<long> Send(MessageModel message);
		IReadOnlyCollection<MessageModel> GetMessagesByChat(long chatId);
		Task Update(MessageModel message);
		Task Delete(long id);
	}
}