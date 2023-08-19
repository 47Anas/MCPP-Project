using Anas_sBookShelf.Dtos.CustomerDtos;
using Anas_sBookShelf.Entities;
using AutoMapper;

namespace Anas_sBookShelf.WepApi.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerListDto>();
            CreateMap<Customer, CustomerDetailsDto>();

            CreateMap<Customer, CustomerDto>().ReverseMap();

        }

    }
}
