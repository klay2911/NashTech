using AutoMapper;

namespace LibraryManagement.Application.Mapper;

public abstract class BaseProfile<T, TResponse, TRequest> : Profile where T : class
{
    protected BaseProfile()
    {
        CreateMap<T, TResponse>();
        CreateMap<TRequest, T>();
    }
}