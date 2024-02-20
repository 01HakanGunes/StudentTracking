using API.Data;
using API.Models;
using API.Repository.IRepository;

namespace API.Repository
{
	public class InstructorRepository : Repository<Instructor>, IInstructorRepository
	{
		private readonly AppDbContext _db;

		public InstructorRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}
	}
}