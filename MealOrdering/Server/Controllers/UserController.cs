using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("[action]")]
		public async Task<ServiceResponse<List<UserDto>>> GetUsers()
		{
			return new ServiceResponse<List<UserDto>>()
			{
				Value = await _userService.GetUsersAsync()
			};
		}
	}
}
