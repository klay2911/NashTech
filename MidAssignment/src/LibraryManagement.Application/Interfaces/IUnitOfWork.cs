namespace LibraryManagement.Application.Interfaces;

public interface IUnitOfWork
{
    Task CreateTransaction();
    Task SaveAsync();
    Task CommitAsync();
    Task RollBackAsync();
}