using System.Collections.Generic;
using System.Threading.Tasks;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.DataAccess.Repositories;

namespace BsiMobile.Web.Domain.Services.Chats
{
	public class ChatsService : IChatsService
	{
		#region DI и конструктор

		private readonly IDbRepository _dbRepository;
		
		public ChatsService(IDbRepository dbRepository)
		{
			_dbRepository = dbRepository;
		}

		#endregion
		
		public async Task<long> Create(List<long> userIds)
		{
			var newChat = await _dbRepository.Add(new Chat());
			foreach (var userId in userIds)
			{
				await _dbRepository.Add(new ChatUsers
				{
					UserId = userId,
					ChatId = newChat
				});
			}

			return newChat;
		}
	}
}