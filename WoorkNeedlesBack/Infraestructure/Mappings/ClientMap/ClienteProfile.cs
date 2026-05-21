using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings.ClientMap
{
    public class ClienteProfile: Profile
    {
        public ClienteProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Cliente, Domain.Entities.Cliente>()
                .ForMember(dest => dest.Pais,
                        opt => opt.MapFrom(src => src.IdpaisNavigation != null
                            ? src.IdpaisNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Departamento,
                        opt => opt.MapFrom(src => src.IddepartNavigation != null
                            ? src.IddepartNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Ciudad,
                        opt => opt.MapFrom(src => src.IdciudadNavigation != null
                            ? src.IdciudadNavigation.Nombre : string.Empty));

            CreateMap<Domain.Entities.Cliente, Infraestructure.Persistence.Models.Cliente>();
        }
    }
}