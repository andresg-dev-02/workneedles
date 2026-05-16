namespace Application.Interfaces;
using Domain.Entities;
public interface IUserRepository
{
    Task<Usuario> GetByEmailAsync(string email);
    Task<Usuario> GetByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task AddAsync(Usuario user);
    Task UpdateAsync(Usuario user);
    Task DeleteAsync(Usuario user);
}