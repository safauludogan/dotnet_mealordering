using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Services.Services
{
	public class OrderService : IOrderService
	{

		private readonly DataContext _context;
		private readonly IMapper _mapper;
		private readonly IValidationService _validationService;

		public OrderService(DataContext context, IMapper mapper, IValidationService validationService)
		{
			_context = context;
			_mapper = mapper;
			_validationService = validationService;
		}


		public async Task<OrderDto> CreateOrderAsync(OrderDto Order)
		{
			var dbOrder = _mapper.Map<Orders>(Order);
			await _context.AddAsync(dbOrder);
			await _context.SaveChangesAsync();

			return _mapper.Map<OrderDto>(dbOrder);
		}

		public async Task DeleteOrderAsync(Guid OrderId)
		{
			var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderId);
			if (orderItem == null)
				throw new Exception("Sub order not found");

			_context.OrderItems.Remove(orderItem);

			await _context.SaveChangesAsync();
		}

		public async Task<OrderDto> GetOrderByIdAsync(Guid Id)
		{
			return await _context.Orders.Where(i => i.Id == Id)
					.ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
					.FirstOrDefaultAsync();
		}

		public async  Task<List<OrderDto>> GetOrdersAsync(DateTime OrderDate)
		{
			return await _context.Orders.Include(i => i.Supplier)
					  .Where(i => i.CreateDate.Date == OrderDate.Date)
					  .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
					  .OrderBy(i => i.CreateDate)
					  .ToListAsync();
		}

		public async Task<OrderDto> UpdateOrderAsync(OrderDto Order)
		{
			var dbOrder = await _context.Orders.FirstOrDefaultAsync(i => i.Id == Order.Id);
			if (dbOrder == null)
				throw new Exception("Order not found");


			if (!_validationService.HasPermission(dbOrder.CreatedUserId))
				throw new Exception("You cannot change the order unless you created");

			_mapper.Map(Order, dbOrder);
			await _context.SaveChangesAsync();

			return _mapper.Map<OrderDto>(dbOrder);
		}

		public async Task<OrderItemsDto> GetOrderItemsByIdAsync(Guid Id)
		{
			return await _context.OrderItems.Include(i => i.Order).Where(i => i.Id == Id)
					  .ProjectTo<OrderItemsDto>(_mapper.ConfigurationProvider)
					  .FirstOrDefaultAsync();
		}

		public async Task DeleteOrderItemAsync(Guid OrderItemId)
		{
			var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItemId);
			if (orderItem == null)
				throw new Exception("Sub order not found");

			_context.OrderItems.Remove(orderItem);

			await _context.SaveChangesAsync();
		}
		public async Task<List<OrderItemsDto>> GetOrderItemsAsync(Guid OrderId)
		{
			return await _context.OrderItems.Where(i => i.OrderId == OrderId)
					  .ProjectTo<OrderItemsDto>(_mapper.ConfigurationProvider)
					  .OrderBy(i => i.CreateDate)
					  .ToListAsync();
		}
	}
}
