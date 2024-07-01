using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class AppDbContext : IdentityDbContext<IdentityUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		// Each DbSet represents a table
		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Grade> Grades { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Added because of Identity?
			base.OnModelCreating(modelBuilder);

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

			// many courses to many instructors
			modelBuilder.Entity<Instructor>()
				.HasMany(i => i.Courses)
				.WithMany(c => c.Instructors)
				.UsingEntity<Dictionary<string, object>>(
					"InstructorCourses",
					j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
					j => j.HasOne<Instructor>().WithMany().HasForeignKey("InstructorId")
				);

			// Seed initial data
			modelBuilder.Entity<Department>().HasData(
				new Department(name: "Habibi Science", quota: 10) { Id = 1, Description = "Crazy stuff" },
				new Department(name: "History", quota: 10) { Id = 2, Description = "Time traveller" },
				new Department(name: "Physics", quota: 10) { Id = 3, Description = "Be bald" }
			);

			modelBuilder.Entity<Student>().HasData(
				new Student(name: "Kontishot The Dreamer", number: 10, departmentId: 1) { Id = 1, Number = 10 },
				new Student(name: "Darkness Rises", number: 15, departmentId: 2) { Id = 2, Number = 20 },
				new Student(name: "Speed of Light", number: 20, departmentId: 3) { Id = 3, Number = 30 }
			);

			modelBuilder.Entity<Instructor>().HasData(
				new Instructor(name: "Professor Gopkins") { Id = 1 },
				new Instructor(name: "Kül Yutmaz") { Id = 2 },
				new Instructor(name: "Albert Einstein") { Id = 3 }
			);

			modelBuilder.Entity<Course>().HasData(
				new Course(code: 100, name: "Introduction to Teleportation", quota: 10, departmentId: 1) { Id = 1, Description = "Really cool class 1" },
				new Course(code: 101, name: "How to Touch Grass 101", quota: 10, departmentId: 2) { Id = 2, Description = "Really cool class 2" },
				new Course(code: 102, name: "Science of University Life", quota: 10, departmentId: 3) { Id = 3, Description = "Really cool class 3" }
			);

			modelBuilder.Entity<Grade>().HasData(
				new Grade(studentId: 1, courseId: 1) { Id = 1, Midterm = 20, Final = 30, Homework = 40 },
				new Grade(studentId: 2, courseId: 2) { Id = 2, Midterm = 52, Final = 20, Homework = 50 },
				new Grade(studentId: 3, courseId: 3) { Id = 3, Midterm = 30, Final = 99, Homework = 60 }
			);
		}
	}
}