using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Productos;
using Domain.Entities;

namespace Application.Mappings.ProductoMap
{
    public class ColorTallaProductoDtoProfile : Profile
    {
        public ColorTallaProductoDtoProfile()
        {
            CreateMap<Domain.Entities.ProductoColore, ProductoColorDto>();
            CreateMap<Domain.Entities.ProductoTalla, ProductoTallaDto>();
        }
    }
}