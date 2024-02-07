
namespace MealOrdering.Server.Data.Models
{
	public class Suppliers
	{
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string WebURL { get; set; }
        public bool IsActive { get; set; }
		public virtual ICollection<Orders> Orders { get; set; }
	}
}
