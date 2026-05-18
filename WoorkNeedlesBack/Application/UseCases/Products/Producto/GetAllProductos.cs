using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Application.Interfaces.Product;
using Domain.Entities;
using AutoMapper;

namespace Application.UseCases.Products.Producto
{
    public class GetAllProductos(IProductoRepository repository, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoDto>> TraerProductos()
        {
            var productos = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProductoDto>>(productos);
        }
    }
}