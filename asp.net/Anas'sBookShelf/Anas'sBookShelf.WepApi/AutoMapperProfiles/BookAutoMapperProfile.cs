﻿using Anas_sBookShelf.Dtos.BookDtos;
using Anas_sBookShelf.Entities;
using AutoMapper;

namespace Anas_sBookShelf.WepApi.AutoMapperProfiles
{
    public class BookAutoMapperProfile : Profile
    {
        public BookAutoMapperProfile()
        {
            CreateMap<Book, BookListDto>();
            CreateMap<Book, BookDetailsDto>();

            CreateMap<Book, BookDto>().ReverseMap();

        }
    }
}