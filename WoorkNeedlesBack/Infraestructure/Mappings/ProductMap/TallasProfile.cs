using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings.ProductMap
{
    public class TallasProfile: Profile
    {
        public TallasProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Talla, Domain.Entities.Talla>();
            CreateMap<Domain.Entities.Talla, Infraestructure.Persistence.Models.Talla>();

            CreateMap<Infraestructure.Persistence.Models.ProductoTalla, Domain.Entities.ProductoTalla>();
            CreateMap<Domain.Entities.ProductoTalla, Infraestructure.Persistence.Models.ProductoTalla>();

        }
    }
}