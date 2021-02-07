using System.Text.Json.Serialization;

namespace BsiMobile.Web.DataAccess.Entities
{
	public class Message : BaseEntity
	{
		public long UserId { get; set; }
		
		[JsonIgnore]
		public User User { get; set; }
		
		public long ChatId { get; set; }
		
		[JsonIgnore]
		public Chat Chat { get; set; }

		[JsonIgnore]
		public byte[] EncryptedText { get; set; }
	}
}