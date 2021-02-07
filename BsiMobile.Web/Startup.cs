using System.Collections.Generic;
using BsiMobile.Web.DataAccess;
using BsiMobile.Web.DataAccess.Repositories;
using BsiMobile.Web.Domain.Services.Chats;
using BsiMobile.Web.Domain.Services.Messages;
using BsiMobile.Web.Domain.Services.Users;
using BsiMobile.Web.Helpers;
using BsiMobile.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BsiMobile.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddNewtonsoftJson(options =>
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				);
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo {Title = "BSI API", Version = "v1"});
				c.AddSecurityDefinition("Bearer",
					new OpenApiSecurityScheme
					{
						In = ParameterLocation.Header,
						Description = "Please enter into field the word 'Bearer' following by space and JWT",
						Name = "Authorization", Type = SecuritySchemeType.ApiKey
					});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});
			});
			
			services.AddDbContext<DataContext>(options =>
			{
				options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
			});
			
			services.AddAutoMapper(typeof(Startup));
			
			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidIssuer = AuthOptions.Issuer,
						ValidateAudience = false,
						ValidAudience = AuthOptions.Audience,
						ValidateLifetime = false,
						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
						ValidateIssuerSigningKey = false
					};
				});
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<ICurrentUser, CurrentUser>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IDbRepository, DbRepository>();
			services.AddScoped<IMessagesService, MessagesService>();
			services.AddScoped<IChatsService, ChatsService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
				
			#region Swagger

			app.UseSwagger();
			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("/swagger/v1/swagger.json", "BSI API v1");
				x.RoutePrefix = "swagger";
			});

			#endregion
			
			#region Cors

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			#endregion
			
			#region Authorization

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<JwtMiddleware>();

			#endregion

			#region Endpoints

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			#endregion
		}
	}
}