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
    public class UpdateProducto(IProductoRepository repository)
    {
        public async Task ActualizarProducto(int id, UpdateProductoDto dto)
        {
            var producto = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Producto no encontrado.");
            producto.Actualizar(
                dto.Nombre, dto.Descripcion, dto.Material,
                dto.Preciobase, dto.Urlimagen, dto.IdCategoria);
            await repository.UpdateAsync(producto);
        }
    }
}