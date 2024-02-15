using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
	[ApiController]
	[Route("api/department")]
	public class DepartmentController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IDepartmentRepository repo;

		public DepartmentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			repo = _unitOfWork.departmentRepo;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Department>> GetDepartments()
		{
			return Ok(repo.GetAll());
		}

		[HttpGet("{id}", Name = "GetDepartment")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Department> GetDepartment(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Department? Department = repo.Get(s => s.Id == id);

			if (Department == null)
			{
				return NotFound();
			}

			return Ok(Department);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult AddDepartment(DepartmentDTO newDepartment)
		{
			if (newDepartment == null || newDepartment.Id > 0)
			{
				return BadRequest(newDepartment);
			}

			if (repo.Get(s => s.Name == newDepartment.Name) != null)
			{
				ModelState.AddModelError("", "Department already exists!");

				return BadRequest(ModelState);
			}

			Department model = new()
			{
				Id = newDepartment.Id,
				Name = newDepartment.Name,
				Description = newDepartment.Description,
				Quota = newDepartment.Quota
			};

			repo.Add(model);
			_unitOfWork.Save();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult RemoveDepartment(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Department? Department = repo.Get(s => s.Id == id);

			if (Department == null)
			{
				return NotFound();
			}

			repo.Remove(Department);
			_unitOfWork.Save();

			return NoContent();
		}
	}
}