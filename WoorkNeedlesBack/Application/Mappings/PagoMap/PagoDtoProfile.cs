using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Pago;

namespace Application.Mappings.PagoMap
{
    public class PagoDtoProfile : Profile
    {
        public PagoDtoProfile()
        {
            CreateMap<Domain.Entities.Pago, PagoDto>();
        }
    }
}