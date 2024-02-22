using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
	public class CourseDTO
	{
		public int Code { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int Quota { get; set; }
		public int DepartmentId { get; set; }
	}
}