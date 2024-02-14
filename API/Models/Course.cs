using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Course
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Instructor { get; set; }
		public int Quota { get; set; }

		// one-to-many
		public ICollection<Grade>? Grades { get; set; }

		// many-to-many
		public ICollection<Student>? Students { get; set; }

		// Foreign keys
		public int DepartmentId { get; set; }

		// Navigation Properties
		public Department? Department { get; set; }
	}
}