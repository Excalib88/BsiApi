using System.Collections.Generic;
using System.Threading.Tasks;
using BsiMobile.Web.Domain.Services.Chats;
using BsiMobile.Web.Domain.Services.Messages;
using Microsoft.AspNetCore.Mvc;

namespace BsiMobile.Web.Controllers
{
	public class ChatController : BaseController
	{
		#region DI и конструктор

		private readonly IMessagesService _messagesService;
		private readonly IChatsService _chatsService;
		
		public ChatController(IMessagesService messagesService, IChatsService chatsService)
		{
			_messagesService = messagesService;
			_chatsService = chatsService;
		}

		#endregion

		[HttpPost("new")]
		public async Task<IActionResult> NewChat(List<long> userIds)
		{
			var result = await _chatsService.Create(userIds);
			return Ok();
		}

		[HttpPost("send")]
		public async Task<IActionResult> SendMessage(MessageModel messageModel)
		{
			var result = await _messagesService.Send(messageModel);

			return Ok(result);
		}

		[HttpGet("{chatId}")]
		public IActionResult GetMessages(long chatId)
		{
			var messages = _messagesService.GetMessagesByChat(chatId);
			return Ok(messages);
		}
	}
}