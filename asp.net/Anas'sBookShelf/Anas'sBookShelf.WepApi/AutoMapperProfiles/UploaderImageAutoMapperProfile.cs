using Anas_sBookShelf.Dtos.Uploaders;
using Anas_sBookShelf.Entities.Books;
using Anas_sBookShelf.Entities.Cutomer;
using AutoMapper;

namespace Anas_sBookShelf.WepApi.AutoMapperProfiles
{
    public class UploaderImageAutoMapperProfile : Profile
    {
        public UploaderImageAutoMapperProfile()
        {
            CreateMap<UploaderImageDto, CustomerImage>().ReverseMap();
            CreateMap<UploaderImageDto, BookImage>().ReverseMap();
        }
    }
}
