using AutoMapper;
using BookStoreDomain.Entities;
using BookStoreDomain.Entities.Dtos;

namespace BookStoreApplication.Mappers
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
        }
    }
}