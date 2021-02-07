using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BsiMobile.Web.DataAccess.Entities;
using BsiMobile.Web.DataAccess.Repositories;
using BsiMobile.Web.Helpers;
using Microsoft.Extensions.Configuration;

namespace BsiMobile.Web.Domain.Services.Users
{
	public class UserService : IUserService
	{
		public AuthenticateResponse GetInfo(string username)
		{
			var user = _dbRepository
				.GetAll<User>()
				.FirstOrDefault(x => x.Username == username);

			if (user == null) throw new ClientErrorException("User was not found");

			return new AuthenticateResponse(user);
		}

		public AuthenticateResponse Authenticate(AuthenticateRequest model)
		{
			var user = _dbRepository
				.GetAll<User>()
				.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

			if (user == null)
				// todo: need to add logger
				throw new UnauthorizedAccessException("Username or password is incorrect");

			var token = _configuration.GenerateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		public AesKeyModel GetAesKey(long userId)
		{
			var user = _dbRepository.GetById<User>(userId);

			return new AesKeyModel
			{
				Key = user.Key,
				Iv = user.Iv
			};
		}

		public async Task<AuthenticateResponse> Register(UserModel userModel)
		{
			var user = _mapper.Map<User>(userModel);

			var aesKey = CryptHelper.GenerateAesKeys();

			user.Key = aesKey.Key;
			user.Iv = aesKey.Iv;

			await _dbRepository.Add(user);

			var response = Authenticate(new AuthenticateRequest
			{
				Username = user.Username,
				Password = user.Password
			});

			return response;
		}

		public User GetById(int id)
		{
			return _dbRepository.GetById<User>(id);
		}

		public IEnumerable<User> GetAll()
		{
			return _dbRepository.GetAll<User>();
		}

		#region DI, ctor

		private readonly IDbRepository _dbRepository;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;

		public UserService(IDbRepository dbRepository, IConfiguration configuration, IMapper mapper)
		{
			_dbRepository = dbRepository;
			_configuration = configuration;
			_mapper = mapper;
		}

		#endregion
	}
}