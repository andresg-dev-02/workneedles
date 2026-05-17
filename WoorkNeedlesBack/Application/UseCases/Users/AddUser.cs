using Application.Interfaces;
using Application.DTOs.UserModel;
using Domain.Entities;
using Domain.Ports.Output;

namespace Application.UseCases.Users;

public class AddUser(IUserRepository userRepository, IPasswordHash passwordHash)
{
    public async Task AddNewUser(CreateUsuarioDto usuariodto)
    {
        var contrasenaHash = passwordHash.Hashear(usuariodto.Contrasena);
        var usuario = Usuario.Crear(usuariodto.Nombres,usuariodto.Apellidos,usuariodto.Email,contrasenaHash,usuariodto.Telefono,usuariodto.IdRol,usuariodto.IdPais,usuariodto.IdCiudad);

        await userRepository.AddAsync(usuario);
    }
}