using AutoMapper;
using Domain.Entities;
using Application.DTOs.Client;

namespace Application.Mappings.ClienteMap
{
    public class ClienteDtoProfile: Profile
    {
        public ClienteDtoProfile()
        {
            CreateMap<Domain.Entities.Cliente, ClienteDto>();
        }
    }
}