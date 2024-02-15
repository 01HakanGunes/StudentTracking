using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
	[ApiController]
	[Route("api/grade")]
	public class GradeController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGradeRepository repo;

		public GradeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			repo = _unitOfWork.gradeRepo;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Grade>> GetGrades()
		{
			return Ok(repo.GetAll());
		}

		[HttpGet("{id}", Name = "GetGrade")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Grade> GetGrade(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Grade? Grade = repo.Get(s => s.Id == id);

			if (Grade == null)
			{
				return NotFound();
			}

			return Ok(Grade);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult AddGrade(GradeDTO newGrade)
		{
			if (newGrade == null || newGrade.Id > 0)
			{
				return BadRequest(newGrade);
			}

			Grade model = new()
			{
				Id = newGrade.Id,
				Midterm = newGrade.Midterm,
				Final = newGrade.Final,
				Homework = newGrade.Homework,
				StudentId = newGrade.StudentId,
				CourseId = newGrade.CourseId
			};

			repo.Add(model);
			_unitOfWork.Save();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult RemoveGrade(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Grade? Grade = repo.Get(s => s.Id == id);

			if (Grade == null)
			{
				return NotFound();
			}

			repo.Remove(Grade);
			_unitOfWork.Save();

			return NoContent();
		}
	}
}