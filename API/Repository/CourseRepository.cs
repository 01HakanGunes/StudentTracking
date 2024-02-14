using API.Data;
using API.Models;
using API.Repository.IRepository;

namespace API.Repository
{
	public class CourseRepository : Repository<Course>, ICourseRepository
	{
		private readonly AppDbContext _db;

		public CourseRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}
	}
}