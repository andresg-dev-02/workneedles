using Application.DTOs;
using Application.Interfaces;

namespace Application.UseCases.Usuarios;

public class GetAllUsers(IUserRepository usuarioRepository)
{
    public async Task<IEnumerable<UsuarioDto>> TraerUsuarios()
    {
        var usuarios = await usuarioRepository.GetAllAsync();

        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nombres = u.Nombres,
            Email = u.Email
        });
    }
}