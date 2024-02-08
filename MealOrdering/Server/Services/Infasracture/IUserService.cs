using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;

namespace MealOrdering.Server.Services.Infasracture
{
	public interface IUserService
	{
		public Task<UserLoginResponseDto> Login(string Email, string Password);
		public Task<UserDto?> GetUserById(Guid Id);
		public Task<List<UserDto>> GetUsersAsync();
		public Task<UserDto> CreateUser(UserDto User);
		public Task<UserDto> UpdateUser(UserDto User);
		public Task<bool> DeleteUserById(Guid Id);
	}
}
