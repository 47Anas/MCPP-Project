using Anas_sBookShelf.Entities;
using AutoMapper;
using Anas_sBookShelf.Dtos.OrderDtos;

namespace Anas_sBookShelf.WepApi.AutoMapperProfiles
{
    public class OrderAutoMapperProfiles : Profile
    {
        public OrderAutoMapperProfiles()
        {
            CreateMap<Order, OrderListDto>();

            CreateMap<Order, OrderDetailsDto>();

            CreateMap<OrderDto, Order>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.BookIds, opts => opts.MapFrom(src => src.Books.Select(p => p.Id)));

        }
    }
}
