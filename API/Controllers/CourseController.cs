using API.Models;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
	[ApiController]
	[Route("api/course")]
	public class CourseController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICourseRepository repo;

		public CourseController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			repo = _unitOfWork.courseRepo;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Course>> GetCourses()
		{
			return Ok(repo.GetAll());
		}

		[HttpGet("{id}", Name = "GetCourse")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Course> GetCourse(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Course? Course = repo.Get(s => s.Id == id);

			if (Course == null)
			{
				return NotFound();
			}

			return Ok(Course);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult AddCourse(Course newCourse)
		{
			if (newCourse == null || newCourse.Id > 0)
			{
				return BadRequest(newCourse);
			}

			if (repo.Get(s => s.Name == newCourse.Name) != null)
			{
				ModelState.AddModelError("", "Course already exists!");

				return BadRequest(ModelState);
			}

			repo.Add(newCourse);
			_unitOfWork.Save();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult RemoveCourse(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Course? Course = repo.Get(s => s.Id == id);

			if (Course == null)
			{
				return NotFound();
			}

			repo.Remove(Course);
			_unitOfWork.Save();

			return NoContent();
		}
	}
}