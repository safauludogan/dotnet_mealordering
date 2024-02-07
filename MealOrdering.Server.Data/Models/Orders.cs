namespace MealOrdering.Server.Data.Models
{
	public class Orders
	{
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual Users CreateUser { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
