using Application.DTOs.UserModel;
using Domain.Entities;
using Domain.Ports.Output;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Users;

public class AddUser(IUnitOfWork unitofwork, IPasswordHash passwordHash)
{
    public async Task Execute(CreateUsuarioDto crearUsuariodto)
    {
        var contrasenaHash = passwordHash.Hashear(crearUsuariodto.Contrasena);
        var usuario = Usuario.Crear(crearUsuariodto.Nombres, crearUsuariodto.Apellidos, crearUsuariodto.Email,
            contrasenaHash, crearUsuariodto.Telefono, crearUsuariodto.IdRol, crearUsuariodto.IdPais, crearUsuariodto.IdCiudad);
        await unitofwork.Usuarios.AddAsync(usuario);
        await unitofwork.SaveAsync();
    }
}