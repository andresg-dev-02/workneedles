using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using AutoMapper;
using Application.DTOs.Productos;

namespace Application.Mappings.ProductoMap
{
    public class InsumosDtoProfile : Profile
    {
        public InsumosDtoProfile()
        {
            CreateMap<Domain.Entities.CategoriaInsumo, CategoriaInsumoDto>();
            CreateMap<Domain.Entities.Insumo, InsumoDto>();
            CreateMap<Domain.Entities.InsumosProducto, InsumosProductoDto>();
        }
    }
}