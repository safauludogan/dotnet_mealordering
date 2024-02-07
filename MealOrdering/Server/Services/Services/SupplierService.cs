using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MealOrdering.Server.Services.Services
{
	public class SupplierService : ISupplierService
	{
		public readonly DataContext _context;
		public readonly IMapper _mapper;

		public SupplierService(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<SupplierDto> CreateSupplierAsync(SupplierDto supplier)
		{
			var dbSupplier = _mapper.Map<Suppliers>(supplier);
			await _context.AddAsync(dbSupplier);
			await _context.SaveChangesAsync();
			return _mapper.Map<SupplierDto>(dbSupplier);
		}

		public async Task DeleteSupplierAsync(Guid Id)
		{
			var dbSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == Id);
			if (dbSupplier == null)
				throw new Exception("Restorant Bulunamadı!");

			int orderCount = await _context.Suppliers.Include(i=>i.Orders).Select(i=>i.Orders.Count).FirstOrDefaultAsync();

			if (orderCount > 0)
				throw new Exception($"Silmeye çalıştığınız restorant için oluşturulmuş {orderCount} adet sipariş mevcut.");

			_context.Remove(dbSupplier);

			await _context.SaveChangesAsync();
		}

		public async Task<SupplierDto> GetSupplierByIdAsync(Guid Id)
		{
			return await _context.Suppliers.ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
				.OrderBy(i => i.CreateDate).FirstOrDefaultAsync();
		}

		public async Task<List<SupplierDto>> GetSuppliersAsync()
		{
			return await _context.Suppliers.ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
				.OrderBy(i => i.CreateDate)
				.ToListAsync();
		}

		public async Task<SupplierDto> UpdateSupplierAsync(SupplierDto supplier)
		{
			var dbSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == supplier.Id);
			if (dbSupplier == null)
				throw new Exception("Restorant Bulunamadı!");

			_mapper.Map(supplier, dbSupplier);
			await _context.SaveChangesAsync();
			return _mapper.Map<SupplierDto>(dbSupplier);
		}
	}
}
