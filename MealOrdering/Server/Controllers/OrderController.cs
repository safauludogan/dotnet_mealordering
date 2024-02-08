using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService orderService;
		public OrderController(IOrderService OrderService)
		{
			orderService = OrderService;
		}

		[HttpGet("OrderById/{Id}")]
		public async Task<ServiceResponse<OrderDto>> GetOrderById(Guid Id)
		{
			return new ServiceResponse<OrderDto>()
			{
				Value = await orderService.GetOrderByIdAsync(Id)
			};
		}

		[HttpGet("OrdersByDate")]
		public async Task<ServiceResponse<List<OrderDto>>> GetOrder(DateTime OrderDate)
		{
			return new ServiceResponse<List<OrderDto>>()
			{
				Value = await orderService.GetOrdersAsync(OrderDate)
			};
		}

		[HttpPost("CreateOrder")]
		public async Task<ServiceResponse<OrderDto>> CreateOrder(OrderDto Order)
		{
			return new ServiceResponse<OrderDto>()
			{
				Value = await orderService.CreateOrderAsync(Order)
			};
		}

		[HttpPost("UpdateOrder")]
		public async Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto Order)
		{
			return new ServiceResponse<OrderDto>()
			{
				Value = await orderService.UpdateOrderAsync(Order)
			};
		}

		[HttpPost("DeleteOrder")]
		public async Task<BaseResponse> DeleteOrder([FromBody] Guid OrderId)
		{
			await orderService.DeleteOrderAsync(OrderId);
			return new BaseResponse();
		}

		[HttpGet("OrderItemsById/{Id}")]
		public async Task<ServiceResponse<OrderItemsDto>> GetOrderItemsById(Guid Id)
		{
			return new ServiceResponse<OrderItemsDto>()
			{
				Value = await orderService.GetOrderItemsByIdAsync(Id)
			};
		}


		[HttpPost("DeleteOrderItem")]
		public async Task<BaseResponse> DeleteOrderItem([FromBody] Guid OrderItemId)
		{
			await orderService.DeleteOrderItemAsync(OrderItemId);
			return new BaseResponse();
		}

		[HttpGet("OrderItems")]
		public async Task<ServiceResponse<List<OrderItemsDto>>> GetOrderItems(Guid OrderId)
		{
			return new ServiceResponse<List<OrderItemsDto>>()
			{
				Value = await orderService.GetOrderItemsAsync(OrderId)
			};
		}

	}
}
