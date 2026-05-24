using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Geo;
using Domain.Entities;

namespace Application.Mappings.GeoMap
{
    public class UbicationDtoProfile : Profile
    {
        public UbicationDtoProfile()
        {
            CreateMap<Domain.Entities.Ciudade, CiudadDto>();
            CreateMap<CiudadDto, Domain.Entities.Ciudade>();
        }
    }
}