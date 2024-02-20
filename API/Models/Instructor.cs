using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Instructor
	{
		// Simple Properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		// one-to-many

		// many-to-many
		public ICollection<Course> Courses { get; set; }

		// Initialize the relations
		public Instructor(string name)
		{
			Name = name;

			Courses = new List<Course>();
		}
	}
}