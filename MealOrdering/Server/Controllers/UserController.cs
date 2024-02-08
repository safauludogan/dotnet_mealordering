using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("[action]")]
		[AllowAnonymous]
		public async Task<ServiceResponse<UserLoginResponseDto>> Login(UserLoginRequestDto userLoginRequest)
		{

			return new ServiceResponse<UserLoginResponseDto>()
			{
				Value = await _userService.Login(userLoginRequest.Email, userLoginRequest.Password)
			};
		}

		[HttpGet("[action]")]
		public async Task<ServiceResponse<List<UserDto>>> GetUsers()
		{
			return new ServiceResponse<List<UserDto>>()
			{
				Value = await _userService.GetUsersAsync()
			};
		}

		[HttpPost("[action]")]
		public async Task<ServiceResponse<UserDto>> CreateUser([FromBody] UserDto User)
		{
			return new ServiceResponse<UserDto>()
			{
				Value = await _userService.CreateUser(User)
			};
		}

		[HttpPost("[action]")]
		public async Task<ServiceResponse<UserDto>> UpdateUser([FromBody] UserDto User)
		{
			return new ServiceResponse<UserDto>()
			{
				Value = await _userService.UpdateUser(User)
			};
		}

		[HttpGet("[action]/{Id}")]
		public async Task<ServiceResponse<UserDto>> GetUserById(Guid Id)
		{
			return new ServiceResponse<UserDto>()
			{
				Value = await _userService.GetUserById(Id)
			};
		}


		[HttpPost("[action]")]
		public async Task<ServiceResponse<bool>> DeleteUser([FromBody] Guid id)
		{
			return new ServiceResponse<bool>()
			{
				Value = await _userService.DeleteUserById(id)
			};
		}
	}
}
