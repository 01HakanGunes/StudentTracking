using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Number { get; set; }

		// one-to-many
		public ICollection<Grade>? Grades { get; set; }

		// many-to-many
		public ICollection<Course>? EnrolledCourses { get; set; }

		// Foreign keys
		public int DepartmentId { get; set; }

		// Navigation Properties
		public Department? Department { get; set; }
	}
}
