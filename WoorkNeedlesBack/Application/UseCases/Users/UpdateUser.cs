using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.User;
using Application.DTOs.UserModel;
using Domain.Ports.Output;

namespace Application.UseCases.Users
{
    public class UpdateUser(IUserRepository userRepository, IPasswordHash passwordHash  )
    {
         public async Task ActualizarUsuarioAsync(int id, UpdateUsuarioDto actualizaruserdto)
    {
        var usuario = await userRepository.GetByIdAsync(id);
        var ContrasenaNueva = string.IsNullOrWhiteSpace(actualizaruserdto.Contrasena) ? null : passwordHash.Hashear(actualizaruserdto.Contrasena);

        usuario.Actualizar(actualizaruserdto.Nombres,actualizaruserdto.Apellidos,actualizaruserdto.Email,actualizaruserdto.Telefono,actualizaruserdto.IdRol,actualizaruserdto.IdPais,actualizaruserdto.IdCiudad,ContrasenaNueva
        );

        await userRepository.UpdateAsync(usuario);
    }
    }
}