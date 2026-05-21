using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Pedido;

namespace Application.Mappings.PedidoMap
{
    public class PedidoDetalleDtoProfile : Profile
    {
        public PedidoDetalleDtoProfile()
        {
            CreateMap<Domain.Entities.DetallePedido, DetallePedidoDto>();
            CreateMap<Domain.Entities.HistorialPedido, HistorialPedidoDto>();
            CreateMap<Domain.Entities.Devolucione, DevolucionesDto>();
        }
    }
}