﻿namespace BsiMobile.Web.Domain.Services.Messages
{
	public class MessageModel
	{
		public long UserId { get; set; }
		
		public long ChatId { get; set; }
		
		public string Text { get; set; }
		public byte[] EncryptedText { get; set; }
	}
}