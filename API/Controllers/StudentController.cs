using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
	[ApiController]
	[Route("api/student")]
	public class StudentController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public StudentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
		{
			return Ok(await _unitOfWork.studentRepo.GetAllAsync());
		}

		[HttpGet("{id}", Name = "GetStudent")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Student>> GetStudent(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Student? student = await _unitOfWork.studentRepo.GetAsync(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			return Ok(student);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddStudent(StudentDTO newStudent)
		{
			if (newStudent == null || newStudent.Name == null || newStudent.Id > 0)
			{
				return BadRequest(newStudent);
			}

			if (await _unitOfWork.studentRepo.GetAsync(s => s.Number == newStudent.Number) != null)
			{
				ModelState.AddModelError("", "Student already exists!");

				return BadRequest(ModelState);
			}

			Student model = new(newStudent.Name, newStudent.Number, newStudent.DepartmentId)
			{
				Id = newStudent.Id
			};

			_unitOfWork.studentRepo.Add(model);
			await _unitOfWork.SaveAsync();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> RemoveStudent(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Student? student = await _unitOfWork.studentRepo.GetAsync(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			_unitOfWork.studentRepo.Remove(student);
			await _unitOfWork.SaveAsync();

			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDTO)
		{
			if (studentDTO == null || studentDTO.Name == null)
			{
				return NotFound();
			}

			Student model = new(studentDTO.Name, studentDTO.Number, studentDTO.DepartmentId)
			{
				Id = id,
			};

			_unitOfWork.studentRepo.Update(model);
			await _unitOfWork.SaveAsync();
			return Ok();
		}
	}
}