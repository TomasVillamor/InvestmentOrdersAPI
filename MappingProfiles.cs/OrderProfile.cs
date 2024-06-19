using AutoMapper;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.Dtos.Order;

namespace InvestmentOrdersAPI.MappingProfiles.cs;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderReadDto>();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
    }
}
