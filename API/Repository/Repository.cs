using System.Linq.Expressions;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbSet<T> _dbSet;

		public Repository(AppDbContext db)
		{
			_dbSet = db.Set<T>();
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
		{
			T? result = await _dbSet.Where(filter).FirstOrDefaultAsync();

			if (result == null)
			{
				throw new InvalidOperationException("No matching element found.");
			}

			return result;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}
	}
}