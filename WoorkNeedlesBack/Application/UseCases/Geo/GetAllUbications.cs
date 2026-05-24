using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Geo;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;
using AutoMapper;

namespace Application.UseCases.Geo
{
    public class GetAllUbications(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<CiudadDto>> Execute()
        {
            var options = new QueryOptions<Domain.Entities.Ciudade>()
                .AddInclude("IddepartNavigation")
                .AddInclude("IddepartNavigation.IdpaisNavigation");

            var ciudades = await unitofwork.Ubicaciones.GetAllAsync(options);

            return mapper.Map<IEnumerable<CiudadDto>>(ciudades);
        }
    }
}