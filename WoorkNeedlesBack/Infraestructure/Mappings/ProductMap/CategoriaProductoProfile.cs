using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings.ProductMap
{
    public class CategoriaProductoProfile: Profile
    {   
        public CategoriaProductoProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.CategoriaProducto, Domain.Entities.CategoriaProducto>();
            CreateMap<Domain.Entities.CategoriaProducto, Infraestructure.Persistence.Models.CategoriaProducto>();
        }
    }
}