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

            CreateMap<Infraestructure.Persistence.Models.ProductoColore, Domain.Entities.ProductoColore>()
                .ForMember(dest => dest.Nombrecolor,
                        opt => opt.MapFrom(src => src.IdcolorNavigation != null
                            ? src.IdcolorNavigation.Nombre : string.Empty))
                .ForMember(dest => dest.Codigohex,
                        opt => opt.MapFrom(src => src.IdcolorNavigation != null 
                                ? src.IdcolorNavigation.Codigohex : string.Empty));
            CreateMap<Domain.Entities.ProductoColore, Infraestructure.Persistence.Models.ProductoColore>();
        }
    }
}