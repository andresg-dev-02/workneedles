namespace Application.Interfaces.User;
using Domain.Entities;

public interface IUserRepository
{
    Task<Usuario?> GetByEmailAsync(string email);
}