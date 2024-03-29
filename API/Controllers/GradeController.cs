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

		public GradeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
		{
			return Ok(await _unitOfWork.gradeRepo.GetAllAsync());
		}

		[HttpGet("{id}", Name = "GetGrade")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Grade>> GetGrade(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Grade? Grade = await _unitOfWork.gradeRepo.GetAsync(s => s.Id == id);

			if (Grade == null)
			{
				return NotFound();
			}

			return Ok(Grade);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddGrade(GradeDTO newGrade)
		{
			if (newGrade == null || newGrade.Id > 0)
			{
				return BadRequest(newGrade);
			}

			Grade model = new(newGrade.StudentId, newGrade.CourseId)
			{
				Id = newGrade.Id,
				Midterm = newGrade.Midterm,
				Final = newGrade.Final,
				Homework = newGrade.Homework
			};

			_unitOfWork.gradeRepo.Add(model);
			await _unitOfWork.SaveAsync();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> RemoveGrade(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Grade? Grade = await _unitOfWork.gradeRepo.GetAsync(s => s.Id == id);

			if (Grade == null)
			{
				return NotFound();
			}

			_unitOfWork.gradeRepo.Remove(Grade);
			await _unitOfWork.SaveAsync();

			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateGrade(int id, [FromBody] GradeDTO gradeDTO)
		{
			if (gradeDTO == null)
			{
				return NotFound();
			}

			Grade model = new(gradeDTO.StudentId, gradeDTO.CourseId)
			{
				Id = id,
				Midterm = gradeDTO.Midterm,
				Final = gradeDTO.Final,
				Homework = gradeDTO.Homework
			};

			_unitOfWork.gradeRepo.Update(model);
			await _unitOfWork.SaveAsync();
			return Ok();
		}
	}
}