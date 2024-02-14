using API.Data;
using API.Repository.IRepository;

namespace API.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;
		public ICourseRepository courseRepo { get; private set; }
		public IDepartmentRepository departmentRepo { get; private set; }
		public IGradeRepository gradeRepo { get; private set; }
		public IStudentRepository studentRepo { get; private set; }

		public UnitOfWork(AppDbContext db)
		{
			_db = db;
			courseRepo = new CourseRepository(db);
			departmentRepo = new DepartmentRepository(db);
			gradeRepo = new GradeRepository(db);
			studentRepo = new StudentRepository(db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}