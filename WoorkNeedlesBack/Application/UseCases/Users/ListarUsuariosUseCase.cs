using Application.DTOs;
using Domain.Interfaces;

namespace Application.UseCases.Usuarios;

public class ListarUsuariosUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ListarUsuariosUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioDto>> ExecuteAsync()
    {
        var usuarios = await _usuarioRepository.ListarAsync();

        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nombres = u.Nombres,
            Email = u.Email
        });
    }
}