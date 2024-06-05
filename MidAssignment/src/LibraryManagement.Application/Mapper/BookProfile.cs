using AutoMapper;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest=> dest.Request, opt => opt.MapFrom(src => src.BookBorrowingRequestDetails.FirstOrDefault().BookBorrowingRequest));
        CreateMap<BookRequest, Book>();
    }
}