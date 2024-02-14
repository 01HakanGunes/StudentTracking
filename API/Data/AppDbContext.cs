using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Grade> Grades { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure entities for relationships for their corresponding tables

			// one student to many grades
			modelBuilder.Entity<Grade>()
				.HasOne(g => g.Student)
				.WithMany(s => s.Grades)
				.HasForeignKey(g => g.StudentId);

			// one course to many grades
			modelBuilder.Entity<Grade>()
				.HasOne(g => g.Course)
				.WithMany(c => c.Grades)
				.HasForeignKey(g => g.CourseId);

			// one department to many students
			modelBuilder.Entity<Student>()
				.HasOne(s => s.Department)
				.WithMany(d => d.Students)
				.HasForeignKey(s => s.DepartmentId);

			// one department to many courses
			modelBuilder.Entity<Course>()
				.HasOne(c => c.Department)
				.WithMany(d => d.Courses)
				.HasForeignKey(c => c.DepartmentId);

			// many courses to many students
			modelBuilder.Entity<Student>()
				.HasMany(s => s.EnrolledCourses)
				.WithMany(c => c.Students)
				.UsingEntity<Dictionary<string, object>>(
					"StudentCourses",
					j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
					j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId")
				);


			// Initialize data
			modelBuilder.Entity<Student>().HasData(
				new Student { Id = 1, Number = 10, DepartmentId = 3 },
				new Student { Id = 2, Number = 20, DepartmentId = 2 },
				new Student { Id = 3, Number = 30, DepartmentId = 1 }
			);

			modelBuilder.Entity<Course>().HasData(
				new Course { Id = 1, Name = "Ceng100", Description = "Really cool class 1", Instructor = "Mehmet", Quota = 10, DepartmentId = 1 },
				new Course { Id = 2, Name = "Ceng200", Description = "Really cool class 2", Instructor = "Ahmet", Quota = 20, DepartmentId = 2 },
				new Course { Id = 3, Name = "Ceng300", Description = "Really cool class 3", Instructor = "John", Quota = 30, DepartmentId = 3 }
			);

			modelBuilder.Entity<Department>().HasData(
				new Department { Id = 1, Name = "Computer Science", Description = "Crazy stuff", Quota = 10 },
				new Department { Id = 2, Name = "History", Description = "Time traveller", Quota = 20 },
				new Department { Id = 3, Name = "Mechanical Engineering", Description = "Be bald", Quota = 30 }
			);

			modelBuilder.Entity<Grade>().HasData(
				new Grade { Id = 1, Midterm = 20, Final = 30, Homework = 40, StudentId = 1, CourseId = 1 },
				new Grade { Id = 2, Midterm = 52, Final = 20, Homework = 50, StudentId = 2, CourseId = 2 },
				new Grade { Id = 3, Midterm = 30, Final = 99, Homework = 60, StudentId = 3, CourseId = 3 }
			);
		}
	}
}