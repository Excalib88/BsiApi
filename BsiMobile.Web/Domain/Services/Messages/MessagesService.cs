using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.DataAccess.Repositories;
using BsiMobile.Web.Domain.Services.Users;
using BsiMobile.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BsiMobile.Web.Domain.Services.Messages
{
	public class MessagesService : IMessagesService
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly ICurrentUser _currentUser;
		private readonly IDbRepository _dbRepository;
		
		public MessagesService(
			IMapper mapper, 
			IUserService userService, 
			ICurrentUser currentUser, 
			IDbRepository dbRepository)
		{
			_mapper = mapper;
			_userService = userService;
			_currentUser = currentUser;
			_dbRepository = dbRepository;
		}

		public async Task<long> Send(MessageModel message)
		{
			var messageEntity = _mapper.Map<Message>(message);
			var userId = _currentUser.Id;
			var aesKey = _userService.GetAesKey(userId);

			messageEntity.EncryptedText = CryptHelper.Encrypt(message.Text, aesKey);

			var result = await _dbRepository.Add(messageEntity);
			
			return result;
		}

		public IReadOnlyCollection<MessageModel> GetMessagesByChat(long chatId)
		{
			var userId = _currentUser.Id;

			var chat = _dbRepository
				.Get<Chat>()
				.Include(x => x.Users)
				.Include(x => x.Messages)
				.FirstOrDefault(x => x.Id == chatId);

			if (chat?.Users.FirstOrDefault(x => x.Id == userId) != null)
			{
				var messages = _mapper.Map<IReadOnlyCollection<MessageModel>>(chat.Messages);
				var aesKey = _userService.GetAesKey(userId);
				
				foreach (var message in messages)
				{
					message.Text = CryptHelper.Decrypt(message.EncryptedText, aesKey);
				}

				return messages;
			}

			throw new MethodAccessException("Cannot get messages");
		}

		public async Task Update(MessageModel message)
		{
			var entity = _mapper.Map<Message>(message);

			if (entity == null)
			{
				throw new NullReferenceException("Entity was null");
			}
			
			await _dbRepository.Update(entity);
		}

		public async Task Delete(long id)
		{
			await _dbRepository.Delete<Message>(id);
		}
	}
}