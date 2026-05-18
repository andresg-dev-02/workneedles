using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Producto
{
    public class DeleteProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var producto = await unitofwork.Productos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Producto no encontrado.");
            unitofwork.Productos.Delete(producto);
            await unitofwork.SaveAsync();
        }
    }
}