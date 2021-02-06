using System;

namespace BsiMobile.Web.DataAccess.Entities
{
	public interface IEntity
	{
		long Id { get; set; }
		bool IsActive { get; set; }
		DateTime DateCreated { get; set; }
	}
}