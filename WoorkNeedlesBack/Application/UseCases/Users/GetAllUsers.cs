using Application.DTOs.UserModel;
using Application.Interfaces.User;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Users;

public class GetAllUsers(IUnitOfWork unitofwork, IMapper mapper)
{
    public async Task<IEnumerable<UsuarioDto>> Execute()
    {
        var options = new QueryOptions<Domain.Entities.Usuario>()
            .AddInclude("IdrolNavigation")
            .AddInclude("IdpaisNavigation")
            .AddInclude("IdciudadNavigation");

        var usuarios = await unitofwork.Usuarios.GetAllAsync(options);
        return mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
    }
}