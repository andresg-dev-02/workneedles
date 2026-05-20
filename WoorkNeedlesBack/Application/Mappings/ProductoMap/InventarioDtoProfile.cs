using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Productos;

namespace Application.Mappings.ProductoMap
{
    public class InventarioDtoProfile : Profile
    {
        public InventarioDtoProfile()
        {
            CreateMap<Domain.Entities.Inventario, InventarioDto>();
        }
    }
}
