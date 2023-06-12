using Anas_sBookShelf.Dtos.CategoriesDtos;
using Anas_sBookShelf.Entities;
using AutoMapper;

namespace Anas_sBookShelf.WepApi.AutoMapperProfiles
{
    public class CategoriesAutoMapperProfiles :Profile
    {
        public CategoriesAutoMapperProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
