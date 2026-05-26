using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings.PedidoMap
{
    public class PedidoProfile: Profile
    {
        public PedidoProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Pedido, Domain.Entities.Pedido>()
                .ForMember(dest => dest.NombreCliente,
                        opt => opt.MapFrom(src => src.IdclienteNavigation != null
                            ? src.IdclienteNavigation.Nombres + " " + src.IdclienteNavigation.Apellidos
                            : string.Empty))
                .ForMember(dest => dest.NombreUsuario,
                        opt => opt.MapFrom(src => src.IdusuarioNavigation != null
                            ? src.IdusuarioNavigation.Nombres + " " + src.IdusuarioNavigation.Apellidos
                            : string.Empty));

            CreateMap<Domain.Entities.Pedido, Infraestructure.Persistence.Models.Pedido>();
        }
    }
}