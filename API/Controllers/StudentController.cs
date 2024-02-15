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
		private readonly IStudentRepository repo;

		public StudentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			repo = _unitOfWork.studentRepo;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Student>> GetStudents()
		{
			return Ok(repo.GetAll());
		}

		[HttpGet("{id}", Name = "GetStudent")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Student> GetStudent(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Student? student = repo.Get(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			return Ok(student);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult AddStudent(StudentDTO newStudent)
		{
			if (newStudent == null || newStudent.Id > 0)
			{
				return BadRequest(newStudent);
			}

			if (repo.Get(s => s.Number == newStudent.Number) != null)
			{
				ModelState.AddModelError("", "Student already exists!");

				return BadRequest(ModelState);
			}

			Student model = new()
			{
				Id = newStudent.Id,
				Name = newStudent.Name,
				Number = newStudent.Number,
				DepartmentId = newStudent.DepartmentId
			};

			repo.Add(model);
			_unitOfWork.Save();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult RemoveStudent(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Student? student = repo.Get(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			repo.Remove(student);
			_unitOfWork.Save();

			return NoContent();
		}
	}
}