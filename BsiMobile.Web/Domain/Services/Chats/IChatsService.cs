using System.Collections.Generic;
using System.Threading.Tasks;

namespace BsiMobile.Web.Domain.Services.Chats
{
	public interface IChatsService
	{
		Task<long> Create(List<long> userIds);
	}
}