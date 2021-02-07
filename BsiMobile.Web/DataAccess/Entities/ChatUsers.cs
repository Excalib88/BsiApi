namespace BsiMobile.Web.DataAccess.Entities
{
	public class ChatUsers : BaseEntity
	{
		public long ChatId { get; set; }
		public Chat Chat { get; set; }
		public long UserId { get; set; }
		public User User { get; set; }
	}
}