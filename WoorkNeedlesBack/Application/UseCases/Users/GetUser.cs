using Application.DTOs.UserModel;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Users;

public class GetUserById(IUnitOfWork unitofwork, IMapper mapper)
{
    public async Task<UsuarioDto> Execute(int id)
    {
        var options = new QueryOptions<Usuario>()
            .AddInclude("IdrolNavigation")
            .AddInclude("IdpaisNavigation")
            .AddInclude("IdciudadNavigation");

        var usuario = await unitofwork.Usuarios.GetByIdAsync(id, options)
            ?? throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
        return mapper.Map<UsuarioDto>(usuario);
    }
}