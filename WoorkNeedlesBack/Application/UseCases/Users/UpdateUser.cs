using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.DTOs;
using Domain.Ports;

namespace Application.UseCases.Users
{
    public class UpdateUser(IUserRepository userRepository, IPasswordHash passwordHash  )
    {
         public async Task ActualizarUsuarioAsync(int id, UpdateUsuarioDto actualizaruserdto)
    {
        var usuario = await userRepository.GetByIdAsync(id);
         string? ContrasenaNueva = null;
        if (!string.IsNullOrWhiteSpace(actualizaruserdto.ContrasenaNueva))
            ContrasenaNueva = passwordHash.Hashear(actualizaruserdto.ContrasenaNueva);

        usuario.Actualizar(
            actualizaruserdto.Nombres,
            actualizaruserdto.Apellidos,
            actualizaruserdto.Email,
            actualizaruserdto.Telefono,
            actualizaruserdto.IdRol,
            actualizaruserdto.IdPais,
            actualizaruserdto.IdCiudad,
            ContrasenaNueva
        );

        await userRepository.UpdateAsync(usuario);
    }
    }
}