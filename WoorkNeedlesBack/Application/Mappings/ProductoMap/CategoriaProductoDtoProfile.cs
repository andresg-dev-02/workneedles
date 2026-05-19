using AutoMapper;
using Domain.Entities;
using Application.DTOs.Productos;

namespace Application.Mappings.ProductoMap;

public class CategoriaProductoDtoProfile : Profile
{
    public CategoriaProductoDtoProfile()
    {
        CreateMap<Domain.Entities.CategoriaProducto, CategoriaProductoDto>();
    }
}