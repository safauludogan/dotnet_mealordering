using MealOrdering.Shared.DTO;

namespace MealOrdering.Server.Services.Infasracture
{
	public interface IUserService
	{
		public Task<UserDto?> GetUserById(Guid Id);
		public Task<List<UserDto>> GetUsersAsync();
		public Task<UserDto> CreateUser(UserDto User);
		public Task<UserDto> UpdateUser(UserDto User);
		public Task<bool> DeleteUserById(Guid Id);
	}
}
