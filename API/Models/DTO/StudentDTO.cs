using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
	public class StudentDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Number { get; set; }
		public int DepartmentId { get; set; }
	}
}