using AutoMapper;
using Domain.Entities;
using Application.DTOs.Client;

namespace Application.Mappings.ClienteMap
{
    public class ClienteDtoProfile: Profile
    {
        public ClienteDtoProfile()
        {
            CreateMap<Domain.Entities.Cliente, ClienteDto>()
                .ForMember(dest => dest.IdPais, opt => opt.MapFrom(src => src.Idpais))
                .ForMember(dest => dest.IdDepart, opt => opt.MapFrom(src => src.Iddepart))
                .ForMember(dest => dest.IdCiudad, opt => opt.MapFrom(src => src.Idciudad));
        }
    }
}