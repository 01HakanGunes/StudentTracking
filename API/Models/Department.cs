using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Department
	{
		// Simple properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required]
		[Range(0, 1000)]
		public int Quota { get; set; }

		// one-to-many
		public ICollection<Course>? Courses { get; set; }
		public ICollection<Student>? Students { get; set; }

		public Department(string name, int quota)
		{
			Name = name;
			Quota = quota;
			Description = "";

			Courses = new List<Course>();
			Students = new List<Student>();
		}
	}
}