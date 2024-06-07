using AutoMapper;
using LibraryManagement.Application.DTOs.UserDTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapper;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>();
    }
}