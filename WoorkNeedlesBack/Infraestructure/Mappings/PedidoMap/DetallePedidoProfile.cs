using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;

namespace Infraestructure.Mappings.PedidoMap
{
    public class DetallePedidoProfile : Profile
    {
        public DetallePedidoProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.DetallePedido, DetallePedido>()
                .ForMember(dest => dest.NombreProducto,
                        opt => opt.MapFrom(src => src.IdproductoNavigation != null
                            ? src.IdproductoNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Talla,
                        opt => opt.MapFrom(src => src.IdinventarioNavigation != null
                            ? src.IdinventarioNavigation.IdtallaNavigation!.Nombre : string.Empty))
                .ForMember(dest => dest.Color,
                        opt => opt.MapFrom(src => src.IdinventarioNavigation != null
                            ? src.IdinventarioNavigation.IdcolorNavigation!.Nombre : string.Empty));

            CreateMap<DetallePedido, Infraestructure.Persistence.Models.DetallePedido>();



            CreateMap<Infraestructure.Persistence.Models.HistorialPedido, Domain.Entities.HistorialPedido>()
                .ForMember(dest => dest.NombreUsuario,
                        opt => opt.MapFrom(src => src.IdusuarioNavigation != null
                            ? src.IdusuarioNavigation.Nombres + " " + src.IdusuarioNavigation.Apellidos
                            : string.Empty));
            CreateMap<Domain.Entities.HistorialPedido, Infraestructure.Persistence.Models.HistorialPedido>();



            CreateMap<Infraestructure.Persistence.Models.Devolucione, Devolucione>()
                .ForMember(dest => dest.NombreUsuario,
                        opt => opt.MapFrom(src => src.IdusuarioNavigation != null
                            ? src.IdusuarioNavigation.Nombres + " " + src.IdusuarioNavigation.Apellidos
                            : string.Empty))
                .ForMember(dest => dest.NombreCliente,
                        opt => opt.MapFrom(src => src.IdclienteNavigation != null
                            ? src.IdclienteNavigation.Nombres + " " + src.IdclienteNavigation.Apellidos
                            : string.Empty));

            CreateMap<Devolucione, Infraestructure.Persistence.Models.Devolucione>();
        }
    }
}