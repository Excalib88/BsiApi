using System;
using System.Text.Json.Serialization;

namespace BsiMobile.Web.DataAccess.Entities
{
	public class BaseEntity : IEntity
	{
		[JsonIgnore] 
		public long CreatedUserId { get; set; }

		[JsonIgnore]
		public long UpdatedUserId { get; set; }

		public long Id { get; set; }

		public bool IsActive { get; set; } = true;

		[JsonIgnore] 
		public DateTime DateCreated { get; set; } = DateTime.Now;
	}
}