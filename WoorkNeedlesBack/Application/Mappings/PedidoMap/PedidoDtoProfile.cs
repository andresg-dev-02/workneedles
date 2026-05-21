using AutoMapper;
using Domain.Entities;
using Application.DTOs.Pedido;

namespace Application.Mappings.PedidoMap
{
    public class PedidoDtoProfile: Profile
    {
        public PedidoDtoProfile()
        {
            CreateMap<Domain.Entities.Pedido, PedidoDto>();
        }
    }
}