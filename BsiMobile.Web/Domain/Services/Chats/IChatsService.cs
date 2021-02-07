using System.Threading.Tasks;
using BsiMobile.Web.DataAccess.Entities;

namespace BsiMobile.Web.Domain.Services.Chats
{
	public interface IChatsService
	{
		Task<long> SendMessage(Message message);
	}
}