using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Infraestructure.Persistence.Models.Usuario, Domain.Entities.Usuario>()
            .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.IdrolNavigation.Nombre))
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.IdpaisNavigation.Nombre))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.IdciudadNavigation.Nombre));

        CreateMap<Domain.Entities.Usuario, Infraestructure.Persistence.Models.Usuario>();
    }
}