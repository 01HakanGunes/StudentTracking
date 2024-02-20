using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
	public class Grade
	{
		// Simple Properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Range(0, 100)]
		public double Midterm { get; set; }
		[Range(0, 100)]
		public double Final { get; set; }
		[Range(0, 100)]
		public double Homework { get; set; }

		// Foreign keys
		[Required]
		public int StudentId { get; set; }
		[Required]
		public int CourseId { get; set; }

		// Navigation properties
		public Student? Student { get; set; }
		public Course? Course { get; set; }

		public Grade(int studentId, int courseId)
		{
			StudentId = studentId;
			CourseId = courseId;
		}
	}
}