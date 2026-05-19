using AutoMapper;
using Domain.Entities;
using Application.DTOs.Productos;

namespace Application.Mappings.ProductoMap
{
    public class ProductoDtoProfile: Profile
    {
        public ProductoDtoProfile()
        {
            CreateMap<Producto, ProductoDto>();
        }
    }
}