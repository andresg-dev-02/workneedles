using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Models;

namespace Infraestructure.Mappings.ProductMap
{
    public class ColoresProfile: Profile
    {
        public ColoresProfile()
        {
            CreateMap<Infraestructure.Persistence.Models.Colore, Domain.Entities.Colore>();
            CreateMap<Domain.Entities.Colore, Infraestructure.Persistence.Models.Colore>();

            CreateMap<Infraestructure.Persistence.Models.ProductoColore, Domain.Entities.ProductoColore>();
            CreateMap<Domain.Entities.ProductoColore, Infraestructure.Persistence.Models.ProductoColore>();
        }
    }
}