using AutoMapper;
using MealOrdering.Server.Data.Models;
using MealOrdering.Shared.DTO;

namespace MealOrdering.Server.Services.Extensions
{
	public static class ConfigureMappingExtension
	{
		public static IServiceCollection ConfigureMapping(this IServiceCollection service)
		{
			var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

			IMapper mapper = mappingConfig.CreateMapper();

			service.AddSingleton(mapper);

			return service;
		}
	}
}


public class MappingProfile : Profile
{
	public MappingProfile()
	{
		AllowNullDestinationValues = true;
		AllowNullCollections = true;

		CreateMap<Suppliers, SupplierDto>()
			.ReverseMap();

		CreateMap<Users, UserDto>()
			.ReverseMap();

		CreateMap<Orders, UserDto>()
			.ReverseMap();

		CreateMap<Orders, OrderDto>()
			.ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
			.ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreateUser.FirstName + " " + z.CreateUser.LastName));

		CreateMap<OrderDto, Orders>();

		CreateMap<OrderItems, OrderItemsDto>()
			.ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName))
			.ForMember(x => x.OrderName, y => y.MapFrom(z => z.Order.Name));

		CreateMap<OrderItemsDto, OrderItems>();
	}
}