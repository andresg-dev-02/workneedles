using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Geo;
using Domain.Entities;

namespace Infraestructure.Mappings.GeoMap
{
    public class UbicationProfile : Profile
    {
        public UbicationProfile()
        {
            CreateMap<
                Infraestructure.Persistence.Models.Ciudade,
                Domain.Entities.Ciudade
            >()
            .ForMember(
                dest => dest.DepartamentoNombre,
                opt => opt.MapFrom(src => src.IddepartNavigation.Nombre)
            )
            .ForMember(
                dest => dest.PaisNombre,
                opt => opt.MapFrom(src => src.IddepartNavigation.IdpaisNavigation.Nombre)
            );
        }
    }
}