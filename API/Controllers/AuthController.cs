using Microsoft.AspNetCore.Mvc;
using API.Models.DTO;
using System.Threading.Tasks;

namespace API.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDTO registerDto)
		{
			// TODO

			return Ok(); // Placeholder
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO loginDto)
		{
			// TODO

			return Ok(); // Placeholder
		}
	}
}