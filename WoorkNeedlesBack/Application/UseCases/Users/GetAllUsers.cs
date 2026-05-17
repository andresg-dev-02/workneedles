using Application.DTOs.UserModel;
using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases.Users;

public class GetAllUsers(IUserRepository usuarioRepository, IMapper mapper)
{
    public async Task<IEnumerable<UsuarioDto>> TraerUsuarios()
    {
        var usuarios = await usuarioRepository.GetAllAsync();
        return mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
    }

}