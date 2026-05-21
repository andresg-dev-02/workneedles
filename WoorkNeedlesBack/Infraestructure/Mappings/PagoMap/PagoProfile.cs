using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;

namespace Infraestructure.Mappings.PagoMap
{
    public class PagoProfile : Profile
    {
        public PagoProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Pago, Domain.Entities.Pago>()
                .ForMember(dest => dest.NombreUsuario,
                        opt => opt.MapFrom(src => src.IdusuarioNavigation != null
                            ? src.IdusuarioNavigation.Nombres + " " + src.IdusuarioNavigation.Apellidos
                            : string.Empty));
            CreateMap<Domain.Entities.Pago, Infraestructure.Persistence.Models.Pago>();
        }
    }
}