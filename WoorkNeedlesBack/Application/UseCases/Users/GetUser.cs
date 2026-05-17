using Application.DTOs.UserModel;
using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases.Users;

public class GetUserById(IUserRepository userRepository, IMapper mapper)
{
    public async Task<UsuarioDto> Execute(int id)
    {
        var usuario = await userRepository.GetByIdAsync(id);

        if (usuario is null)
            throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");

        return mapper.Map<UsuarioDto>(usuario);
    }
}