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
    public class CreateProducto(IProductoRepository repository)
    {
        public async Task CrearProducto(CreateProductoDto dto)
        {
            var producto = Domain.Entities.Producto.Crear(
                dto.Nombre, dto.Descripcion, dto.Material,
                dto.Preciobase, dto.Urlimagen, dto.IdCategoria);
            await repository.CreateAsync(producto);
        }
    }
}