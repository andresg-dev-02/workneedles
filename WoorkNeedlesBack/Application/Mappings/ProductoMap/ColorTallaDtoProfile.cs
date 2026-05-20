using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Productos;
using Domain.Entities;

namespace Application.Mappings.ProductoMap
{
    public class ColorTallaDtoProfile: Profile
    {
        public ColorTallaDtoProfile()
        {
            CreateMap<Domain.Entities.Colore, ColorDto>();
            CreateMap<Domain.Entities.Talla, TallaDto>();
        }
    }
}