using AutoMapper;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<BookBorrowingRequest, BorrowingRequestResponse>();
    }
}