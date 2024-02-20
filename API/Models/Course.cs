using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Course
	{
		// Simple Properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public int Code { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required]
		[Range(0, 100)]
		public int Quota { get; set; }

		// one-to-many
		public ICollection<Grade> Grades { get; set; }

		// many-to-many
		public ICollection<Student> Students { get; set; }
		public ICollection<Instructor> Instructors { get; set; }

		// Foreign keys
		[Required]
		public int DepartmentId { get; set; }

		// Navigation Properties
		public Department? Department { get; set; }

		// Initialize the relations
		public Course(int code, string name, int quota, int departmentId)
		{
			Code = code;
			Name = name;
			Quota = quota;
			DepartmentId = departmentId;
			Description = "";

			Grades = new List<Grade>();
			Students = new List<Student>();
			Instructors = new List<Instructor>();
		}
	}
}