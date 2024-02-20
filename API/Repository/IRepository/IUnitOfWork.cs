namespace API.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICourseRepository courseRepo { get; }
		IDepartmentRepository departmentRepo { get; }
		IGradeRepository gradeRepo { get; }
		IStudentRepository studentRepo { get; }
		IInstructorRepository instructorRepo { get; }

		Task SaveAsync();
	}
}