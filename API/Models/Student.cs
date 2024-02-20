using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Student
	{
		// Simple properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[Range(0, 10000)]
		public int Number { get; set; }

		// one-to-many
		public ICollection<Grade> Grades { get; set; }

		// many-to-many
		public ICollection<Course> EnrolledCourses { get; set; }

		// Foreign keys
		public int DepartmentId { get; set; }

		// Navigation Properties
		public Department? Department { get; set; }

		public Student(string name, int number, int departmentId)
		{
			Name = name;
			Number = number;
			DepartmentId = departmentId;

			Grades = new List<Grade>();
			EnrolledCourses = new List<Course>();
		}
	}
}
