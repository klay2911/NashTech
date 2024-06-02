using AutoMapper;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class BookProfile : BaseProfile<Book, BookResponse, BookRequest>
{
}