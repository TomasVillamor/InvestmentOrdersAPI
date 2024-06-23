using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.Dtos.Order;

namespace InvestmentOrdersAPI.DataAccess.MappingProfiles.cs;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderReadDto>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset.Name))
            .ForMember(dest => dest.StateDescription, opt => opt.MapFrom(src => src.OrderState.Description));

        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
    }
}
