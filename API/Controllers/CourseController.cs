using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
	[ApiController]
	[Route("api/course")]
	public class CourseController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public CourseController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
		{
			IEnumerable<Course> courses = await _unitOfWork.courseRepo.GetAllAsync();

			return Ok(courses);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Course>> GetCourse(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Course? course = await _unitOfWork.courseRepo.GetAsync(c => c.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return Ok(course);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddCourse(CourseDTO courseDTO)
		{
			if (courseDTO == null || courseDTO.Name == null || courseDTO.Description == null || courseDTO.Instructor == null || courseDTO.Id > 0)
			{
				return BadRequest(courseDTO);
			}

			if (await _unitOfWork.courseRepo.GetAsync(c => c.Name == courseDTO.Name) != null)
			{
				ModelState.AddModelError("", "Course already exists!");
				return BadRequest(ModelState);
			}

			// Convert DTO to model
			Course model = new(courseDTO.Code, courseDTO.Name, courseDTO.Quota, courseDTO.DepartmentId)
			{
				Id = courseDTO.Id,
				Description = courseDTO.Description,
			};

			_unitOfWork.courseRepo.Add(model);
			await _unitOfWork.SaveAsync();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> RemoveCourse(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Course? Course = await _unitOfWork.courseRepo.GetAsync(c => c.Id == id);

			if (Course == null)
			{
				return NotFound();
			}

			_unitOfWork.courseRepo.Remove(Course);
			await _unitOfWork.SaveAsync();

			return NoContent();
		}

		// Add an Endpoint to add students to a course for many to many relation between them.
		[HttpPost("{courseId}/{studentId}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult> AddStudentToCourse(int courseId, int studentId)
		{
			Course? course = await _unitOfWork.courseRepo.GetAsync(c => c.Id == courseId);
			if (course == null)
			{
				return NotFound("Course not found!");
			}

			Student? student = await _unitOfWork.studentRepo.GetAsync(s => s.Id == studentId);
			if (student == null)
			{
				return NotFound("Student not found!");
			}


			course.Students.Add(student);
			student.EnrolledCourses.Add(course);
			await _unitOfWork.SaveAsync();

			return Created();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDTO)
		{
			if (courseDTO == null || courseDTO.Name == null || courseDTO.Description == null || courseDTO.Instructor == null)
			{
				return NotFound();
			}

			Course model = new(courseDTO.Code, courseDTO.Name, courseDTO.Quota, courseDTO.DepartmentId)
			{
				Id = id,
				Description = courseDTO.Description
			};

			_unitOfWork.courseRepo.Update(model);
			await _unitOfWork.SaveAsync();
			return Ok();
		}
	}
}