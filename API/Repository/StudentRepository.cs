using API.Data;
using API.Models;
using API.Repository.IRepository;

namespace API.Repository
{
	public class StudentRepository : Repository<Student>, IStudentRepository
	{
		private readonly AppDbContext _db;

		public StudentRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}
	}
}