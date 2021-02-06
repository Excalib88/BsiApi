using AutoMapper;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.Domain.Services.Users;

namespace BsiMobile.Web
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			// Users
			CreateMap<UserModel, User>()
				.ForMember(dst => dst.Id, opt => opt.Ignore())
				;

			CreateMap<User, AuthenticateResponse>()
				.ForMember(dst => dst.Token, opt => opt.Ignore())
				;
		}
	}
}