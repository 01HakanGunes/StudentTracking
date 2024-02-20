using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
	[ApiController]
	[Route("api/instructor")]
	public class InstructorController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public InstructorController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
		{
			return Ok(await _unitOfWork.instructorRepo.GetAllAsync());
		}

		[HttpGet("{id}", Name = "GetInstructor")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Instructor>> GetInstructor(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Instructor? instructor = await _unitOfWork.instructorRepo.GetAsync(i => i.Id == id);

			if (instructor == null)
			{
				return NotFound();
			}

			return Ok(instructor);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddInstructor(InstructorDTO newInstructor)
		{
			if (newInstructor == null || newInstructor.Name == null || newInstructor.Id > 0)
			{
				return BadRequest(newInstructor);
			}

			Instructor model = new(newInstructor.Name)
			{
				Id = newInstructor.Id
			};

			_unitOfWork.instructorRepo.Add(model);
			await _unitOfWork.SaveAsync();

			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> RemoveInstructor(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			Instructor? instructor = await _unitOfWork.instructorRepo.GetAsync(i => i.Id == id);

			if (instructor == null)
			{
				return NotFound();
			}

			_unitOfWork.instructorRepo.Remove(instructor);
			await _unitOfWork.SaveAsync();

			return NoContent();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateInstructor(int id, [FromBody] InstructorDTO instructorDTO)
		{
			if (instructorDTO == null || instructorDTO.Name == null)
			{
				return NotFound();
			}

			Instructor model = new(instructorDTO.Name)
			{
				Id = id,
			};

			_unitOfWork.instructorRepo.Update(model);
			await _unitOfWork.SaveAsync();
			return Ok();
		}

	}
}