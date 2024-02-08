namespace MealOrdering.Server.Data.Models
{
	public class Users
	{
        public Guid Id { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Orders> Orders{ get; set; }
        public virtual ICollection<OrderItems> CreatedOrderItems { get; set; }
    }
}
