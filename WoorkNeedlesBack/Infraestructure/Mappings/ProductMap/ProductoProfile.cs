using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings.ProductMap
{
    public class ProductoProfile: Profile
    {
        public ProductoProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Producto, Domain.Entities.Producto>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.IdcategoriaNavigation.Nombre));
            CreateMap<Domain.Entities.Producto, Infraestructure.Persistence.Models.Producto>();
        }
    }
}