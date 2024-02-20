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

		public DepartmentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
		{
			return Ok(await _unitOfWork.departmentRepo.GetAllAsync());
		}

		[HttpGet("{id}", Name = "GetDepartment")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Department>> GetDepartment(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Department? Department = await _unitOfWork.departmentRepo.GetAsync(s => s.Id == id);

			if (Department == null)
			{
				return NotFound();
			}

			return Ok(Department);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddDepartment(DepartmentDTO departmentDTO)
		{
			if (departmentDTO == null || departmentDTO.Name == null || departmentDTO.Description == null || departmentDTO.Id > 0)
			{
				return BadRequest(departmentDTO);
			}

			if (await _unitOfWork.departmentRepo.GetAsync(s => s.Name == departmentDTO.Name) != null)
			{
				ModelState.AddModelError("", "Department already exists!");

				return BadRequest(ModelState);
			}

			Department model = new(departmentDTO.Name, departmentDTO.Quota)
			{
				Id = departmentDTO.Id,
				Description = departmentDTO.Description
			};

			_unitOfWork.departmentRepo.Add(model);
			await _unitOfWork.SaveAsync();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> RemoveDepartment(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Department? Department = await _unitOfWork.departmentRepo.GetAsync(s => s.Id == id);

			if (Department == null)
			{
				return NotFound();
			}

			_unitOfWork.departmentRepo.Remove(Department);
			await _unitOfWork.SaveAsync();

			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDTO)
		{
			if (departmentDTO == null || departmentDTO.Name == null || departmentDTO.Description == null)
			{
				return NotFound();
			}

			Department model = new(departmentDTO.Name, departmentDTO.Quota)
			{
				Id = id,
				Description = departmentDTO.Description
			};

			_unitOfWork.departmentRepo.Update(model);
			await _unitOfWork.SaveAsync();
			return Ok();
		}

	}
}