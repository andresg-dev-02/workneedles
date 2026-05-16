using Application.DTOs;
using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases.Usuarios;

public class GetAllUsers(IUserRepository usuarioRepository, IMapper mapper)
{
    public async Task<IEnumerable<UsuarioDto>> TraerUsuarios()
    {
        var usuarios = await usuarioRepository.GetAllAsync();
        return mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
    }
}