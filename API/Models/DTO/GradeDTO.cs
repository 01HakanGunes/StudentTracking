using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
	public class GradeDTO
	{
		public int Id { get; set; }
		public double Midterm { get; set; }
		public double Final { get; set; }
		public double Homework { get; set; }
		public int StudentId { get; set; }
		public int CourseId { get; set; }
	}
}