using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Users;

public class DeleteUser(IUnitOfWork unitofwork)
{
    public async Task Execute(int id)
    {
        var usuario = await unitofwork.Usuarios.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Usuario no encontrado.");
        unitofwork.Usuarios.Delete(usuario);
        await unitofwork.SaveAsync();
    }
}