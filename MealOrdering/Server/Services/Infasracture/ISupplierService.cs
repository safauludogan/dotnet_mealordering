using MealOrdering.Shared.DTO;

namespace MealOrdering.Server.Services.Infasracture
{
	public interface ISupplierService
	{
		Task<List<SupplierDto>> GetSuppliersAsync();
		Task<SupplierDto> GetSupplierByIdAsync(Guid Id);
		Task<SupplierDto> CreateSupplierAsync(SupplierDto supplier);
		Task<SupplierDto> UpdateSupplierAsync(SupplierDto supplier);
		Task DeleteSupplierAsync(Guid Id);

	}
}
