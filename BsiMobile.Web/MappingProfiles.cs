using System.Collections.Generic;
using AutoMapper;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.Domain.Services.Messages;
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
			
			// Chats and Messages
			CreateMap<MessageModel, Message>(MemberList.Source)
				.ForSourceMember(src => src.Text, opt=> opt.DoNotValidate())
				;

			CreateMap<Message, MessageModel>(MemberList.Destination)
				.ForMember(dst => dst.Text, opt => opt.Ignore())
				;

			CreateMap<IReadOnlyCollection<MessageModel>, List<Message>>();

			CreateMap<List<Message>, IReadOnlyCollection<MessageModel>>();
		}
	}
}