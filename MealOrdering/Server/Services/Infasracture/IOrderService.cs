using AutoMapper;
using MealOrdering.Shared.DTO;

namespace MealOrdering.Server.Services.Infasracture
{
	public interface IOrderService
	{
		public Task<OrderDto> CreateOrderAsync(OrderDto Order);
		public Task<OrderDto> UpdateOrderAsync(OrderDto Order);
		public Task DeleteOrderAsync(Guid OrderId);
		public Task<List<OrderDto>> GetOrdersAsync(DateTime OrderDate);
		public Task<OrderDto> GetOrderByIdAsync(Guid Id);
		public Task DeleteOrderItemAsync(Guid OrderItemId);
		public Task<OrderItemsDto> GetOrderItemsByIdAsync(Guid Id);
		public Task<List<OrderItemsDto>> GetOrderItemsAsync(Guid OrderId);
	}
}
