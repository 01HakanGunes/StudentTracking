using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
	public class DepartmentDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int Quota { get; set; }
	}
}