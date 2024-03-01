using AutoMapper;
using BookStoreApp.Svc.Data;
using BookStoreApp.Svc.DTOs.Author;
using BookStoreApp.Svc.DTOs.Book;

namespace BookStoreApp.Svc.Configurations;

public class MapperConfig : Profile
{
  public MapperConfig()
  {
    CreateMap<AuthorDto, Author>().ReverseMap();
    CreateMap<AuthorCreateDto, Author>().ReverseMap();
    CreateMap<AuthorUpdateDto, Author>().ReverseMap();

    CreateMap<Book, BookDto>()
      .ForMember(b => b.AuthorName, a => a.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
      .ReverseMap();
    CreateMap<Book, BookDetailsDto>()
      .ForMember(b => b.AuthorName, a => a.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
      .ReverseMap();
    CreateMap<BookCreateDto, Book>().ReverseMap();
    CreateMap<BookUpdateDto, Book>().ReverseMap();
  }
}
