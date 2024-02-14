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

		public T? Get(Expression<Func<T, bool>> filter)
		{
			return _dbSet.Where(filter).FirstOrDefault();
		}

		public IEnumerable<T> GetAll()
		{
			return _dbSet.ToList();
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}
	}
}