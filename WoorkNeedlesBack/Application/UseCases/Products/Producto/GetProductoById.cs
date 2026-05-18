using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Producto
{
    public class GetProductoById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<ProductoDto> Execute(int id)
        {
            var producto = await unitofwork.Productos.GetByIdAsync(id) ?? throw new KeyNotFoundException("Producto no encontrado.");
            return mapper.Map<ProductoDto>(producto);
        }
    }
}