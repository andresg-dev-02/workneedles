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
    public class UpdateProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateProductoDto actualizarProductodto)
        {
            var producto = await unitofwork.Productos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Producto no encontrado.");
            producto.Actualizar(
                actualizarProductodto.Nombre, actualizarProductodto.Descripcion, actualizarProductodto.Material,
                actualizarProductodto.Preciobase, actualizarProductodto.Urlimagen, actualizarProductodto.IdCategoria);
            unitofwork.Productos.Update(producto);
            await unitofwork.SaveAsync();
        }
    }
}