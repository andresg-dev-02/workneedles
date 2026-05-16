using AutoMapper;
using Domain.Entities;
using Application.DTOs;

namespace Application.Mappings;

public class UsuarioProfileDto : Profile
{
    public UsuarioProfileDto()
    {
        CreateMap<Domain.Entities.Usuario, UsuarioDto>();
        CreateMap<UsuarioDto, Domain.Entities.Usuario>();
    }
}