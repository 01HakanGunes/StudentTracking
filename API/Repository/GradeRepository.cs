using API.Data;
using API.Models;
using API.Repository.IRepository;

namespace API.Repository
{
	public class GradeRepository : Repository<Grade>, IGradeRepository
	{
		private readonly AppDbContext _db;

		public GradeRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}
	}
}