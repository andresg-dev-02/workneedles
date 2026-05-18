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
    public class GetProductoById(IProductoRepository repository, IMapper mapper)
    {
        public async Task<ProductoDto> TraerProductoId(int id)
        {
            var producto = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Producto no encontrado.");
            return mapper.Map<ProductoDto>(producto);
        }
    }
}