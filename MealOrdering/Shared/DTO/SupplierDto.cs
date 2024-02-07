namespace MealOrdering.Shared.DTO
{
	public class SupplierDto
	{
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }
		public string Name { get; set; }
		public string WebURL { get; set; }
		public bool IsActive { get; set; }
	}
}
