﻿using BsiMobile.Web.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BsiMobile.Web.DataAccess
{
	public class DataContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<ChatUsers> ChatUsers { get; set; }
		
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
			
		}
	}
}