using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;

namespace Infraestructure.Mappings.ProductMap
{
    public class InventarioProfile : Profile
    {
        public InventarioProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Inventario, Inventario>()
            .ForMember(dest => dest.NombreProducto,
                       opt => opt.MapFrom(src => src.IdproductoNavigation != null
                           ? src.IdproductoNavigation.Nombre : string.Empty))
            .ForMember(dest => dest.NombreColor,
                       opt => opt.MapFrom(src => src.IdcolorNavigation != null
                           ? src.IdcolorNavigation.Nombre : string.Empty))
            .ForMember(dest => dest.NombreTalla,
                       opt => opt.MapFrom(src => src.IdtallaNavigation != null
                           ? src.IdtallaNavigation.Nombre : string.Empty));

            CreateMap<Inventario, Infraestructure.Persistence.Models.Inventario>();
        }
    }
}