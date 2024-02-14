using API.Data;
using API.Models;
using API.Repository.IRepository;

namespace API.Repository
{
	public class DepartmentRepository : Repository<Department>, IDepartmentRepository
	{
		private readonly AppDbContext _db;

		public DepartmentRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}
	}
}