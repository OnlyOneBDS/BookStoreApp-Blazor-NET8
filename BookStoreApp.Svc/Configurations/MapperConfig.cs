using AutoMapper;
using BookStoreApp.Svc.Data;
using BookStoreApp.Svc.DTOs.Author;

namespace BookStoreApp.Svc.Configurations;

public class MapperConfig : Profile
{
  public MapperConfig()
  {
    CreateMap<AuthorCreateDto, Author>().ReverseMap();
    CreateMap<AuthorUpdateDto, Author>().ReverseMap();
    CreateMap<AuthorDto, Author>().ReverseMap();
  }
}
