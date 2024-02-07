using MealOrdering.Shared.DTO;

namespace MealOrdering.Server.Services.Infasracture
{
	public interface IOrderService
	{
		public Task<OrderDto> CreateOrderAsync(OrderDto Order);
		public Task<OrderDto> UpdateOrderAsync(OrderDto Order);

		public Task DeleteOrderAsync(Guid OrderId);

		public Task<List<OrderDto>> GetOrdersAsync(DateTime OrderDate);

	}
}
