using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BsiMobile.Web.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BsiMobile.Web.DataAccess.Repositories
{
	public class DbRepository : IDbRepository
	{
		private readonly DataContext _context;

		public DbRepository(DataContext context)
		{
			_context = context;
		}

		public IQueryable<T> Get<T>() where T : class, IEntity
		{
			return _context
				.Set<T>()
				.Where(x => x.IsActive)
				.AsQueryable();
		}

		public T GetById<T>(long id) where T : class, IEntity
		{
			return _context
				.Set<T>()
				.FirstOrDefault(x => x.Id == id);
		}

		public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
		{
			return _context
				.Set<T>()
				.Where(selector)
				.Where(x => x.IsActive)
				.AsQueryable();
		}

		public async Task<long> Add<T>(T newEntity) where T : class, IEntity
		{
			var entity = await _context
				.Set<T>()
				.AddAsync(newEntity);

			await SaveChangesAsync();

			return entity.Entity.Id;
		}

		public async Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity
		{
			await _context
				.Set<T>()
				.AddRangeAsync(newEntities);

			await SaveChangesAsync();
		}

		public async Task Delete<T>(long id) where T : class, IEntity
		{
			var activeEntity = await _context
				.Set<T>()
				.FirstOrDefaultAsync(x => x.Id == id);

			activeEntity.IsActive = false;
			await Task.Run(() => _context.Update(activeEntity));

			await SaveChangesAsync();
		}

		public async Task Remove<T>(T entity) where T : class, IEntity
		{
			await Task.Run(() => _context.Set<T>().Remove(entity));
			await SaveChangesAsync();
		}

		public async Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
		{
			await Task.Run(() => _context.Set<T>().RemoveRange(entities));
			await SaveChangesAsync();
		}

		public async Task Update<T>(T entity) where T : class, IEntity
		{
			await Task.Run(() => _context.Set<T>().Update(entity));
			await SaveChangesAsync();
		}

		public async Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity
		{
			await Task.Run(() => _context.Set<T>().UpdateRange(entities));
			await SaveChangesAsync();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public IQueryable<T> GetAll<T>() where T : class, IEntity
		{
			return _context.Set<T>().AsQueryable();
		}
	}
}