using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Domain.Entities.Usuario, Infraestructure.Persistence.Models.Usuario>();
        CreateMap<Infraestructure.Persistence.Models.Usuario, Domain.Entities.Usuario>();
    }
}


