using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Grade
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public double Midterm { get; set; }
		public double Final { get; set; }
		public double Homework { get; set; }

		// Foreign keys
		public int StudentId { get; set; }
		public int CourseId { get; set; }
	
		// Navigation properties
		public Student? Student { get; set; }
		public Course? Course { get; set; }
	}
}