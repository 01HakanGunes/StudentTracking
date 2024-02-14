using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Department
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int Quota { get; set; }

		// one-to-many
		public ICollection<Course>? Courses { get; set; }
		public ICollection<Student>? Students { get; set; }
	}
}