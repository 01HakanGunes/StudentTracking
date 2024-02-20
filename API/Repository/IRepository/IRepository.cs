using System.Linq.Expressions;

namespace API.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		// accesses database so takes long time (hard drive access)
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(Expression<Func<T, bool>> filter);

		// no need for async
		void Add(T entity);
		void Update(T entity);
		void Remove(T entity);
	}
}