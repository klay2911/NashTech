using AutoMapper;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<BookBorrowingRequest, BorrowingRequestResponse>()
            // .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            // .ForMember(dest=>dest.ApproverId, opt=>opt.MapFrom(src=>src.User))
            .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.BookBorrowingRequestDetails.Select(detail => detail.BookId)));
    }
}