namespace MealOrdering.Shared.DTO
{
	public class UserLoginResponseDto
	{
        public string ApiToken { get; set; }
        public UserDto User { get; set; }
    }
}
