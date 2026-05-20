using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using AutoMapper;

namespace Infraestructure.Mappings.ProductMap
{
    public class InsumosProfile : Profile
    {
        public InsumosProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Insumo, Domain.Entities.Insumo>()
            .ForMember(dest => dest.Categoria,
                       opt => opt.MapFrom(src => src.IdcategoriaNavigation != null
                           ? src.IdcategoriaNavigation.Nombre : string.Empty));
            CreateMap<Domain.Entities.Insumo, Infraestructure.Persistence.Models.Insumo>();



            CreateMap<Infraestructure.Persistence.Models.CategoriaInsumo, Domain.Entities.CategoriaInsumo>();
            CreateMap<Domain.Entities.CategoriaInsumo, Infraestructure.Persistence.Models.CategoriaInsumo>();


            CreateMap<Infraestructure.Persistence.Models.InsumosProducto, Domain.Entities.InsumosProducto>()
                .ForMember(dest => dest.NombreProducto,
                        opt => opt.MapFrom(src => src.IdproductoNavigation != null
                            ? src.IdproductoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.NombreInsumo,
                        opt => opt.MapFrom(src => src.IdinsumoNavigation != null
                            ? src.IdinsumoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.UnidadMedida,
                        opt => opt.MapFrom(src => src.IdinsumoNavigation != null
                            ? src.IdinsumoNavigation.Unidadmedida : string.Empty));
            CreateMap<Domain.Entities.InsumosProducto, Infraestructure.Persistence.Models.InsumosProducto>();
        }
    }
}