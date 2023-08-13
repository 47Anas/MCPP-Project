using Anas_sBookShelf.Dtos.CartDtos;
using Anas_sBookShelf.Entities;
using AutoMapper;

namespace Anas_sBookShelf.WepApi.AutoMapperProfiles
{
    public class CartAutoMapperProfile : Profile
    {
        public CartAutoMapperProfile()
        {
            CreateMap<Cart, CartListDto>();
            CreateMap<Cart, CartDetailsDto>();
        }
    }
}
