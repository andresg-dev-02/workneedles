using Application.DTOs.UserModel;
using Domain.Entities;
using Domain.Ports.Output;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Users;

public class UpdateUser(IUnitOfWork unitofwork, IPasswordHash passwordHash)
{
    public async Task Execute(int id, UpdateUsuarioDto actualizarUsuarioDto)
    {
        var options = new QueryOptions<Usuario>()
            .AddInclude("IdrolNavigation")
            .AddInclude("IdpaisNavigation")
            .AddInclude("IdciudadNavigation");

        var usuario = await unitofwork.Usuarios.GetByIdAsync(id, options)
            ?? throw new KeyNotFoundException("Usuario no encontrado.");

        var contrasenaHash = string.IsNullOrWhiteSpace(actualizarUsuarioDto.ContrasenaNueva)
    ? null : passwordHash.Hashear(actualizarUsuarioDto.ContrasenaNueva);

        usuario.Actualizar(actualizarUsuarioDto.Nombres, actualizarUsuarioDto.Apellidos, actualizarUsuarioDto.Email, actualizarUsuarioDto.Telefono, actualizarUsuarioDto.IdRol, actualizarUsuarioDto.IdPais, actualizarUsuarioDto.IdCiudad, contrasenaHash, actualizarUsuarioDto.Activo);

        unitofwork.Usuarios.Update(usuario);
        await unitofwork.SaveAsync();
    }
}