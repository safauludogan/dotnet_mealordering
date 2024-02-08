
namespace MealOrdering.Shared.DTO
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string EmailAddress { get; set; }
		public bool IsActive { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		
	}
}
