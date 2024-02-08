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
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierService supplierService;

		public SupplierController(ISupplierService SupplierService)
		{
			supplierService = SupplierService;
		}



		[HttpGet("SupplierById/{Id}")]
		public async Task<ServiceResponse<SupplierDto>> GetSupplierById(Guid Id)
		{
			return new ServiceResponse<SupplierDto>()
			{
				Value = await supplierService.GetSupplierByIdAsync(Id)
			};
		}


		[HttpGet("Suppliers")]
		public async Task<ServiceResponse<List<SupplierDto>>> GetSuppliers()
		{
			return new ServiceResponse<List<SupplierDto>>()
			{
				Value = await supplierService.GetSuppliersAsync()
			};
		}


		[HttpPost("CreateSupplier")]
		public async Task<ServiceResponse<SupplierDto>> CreateSupplier(SupplierDto Supplier)
		{
			return new ServiceResponse<SupplierDto>()
			{
				Value = await supplierService.CreateSupplierAsync(Supplier)
			};
		}


		[HttpPost("UpdateSupplier")]
		public async Task<ServiceResponse<SupplierDto>> UpdateSupplier(SupplierDto Supplier)
		{
			return new ServiceResponse<SupplierDto>()
			{
				Value = await supplierService.UpdateSupplierAsync(Supplier)
			};
		}


		[HttpPost("DeleteSupplier")]
		public async Task<BaseResponse> DeleteSupplier([FromBody] Guid SupplierId)
		{
			await supplierService.DeleteSupplierAsync(SupplierId);
			return new BaseResponse();
		}
	}
}
