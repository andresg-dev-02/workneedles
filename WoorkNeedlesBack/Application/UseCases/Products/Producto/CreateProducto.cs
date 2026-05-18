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
    public class CreateProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateProductoDto crearProductodto)
        {
            var producto = Domain.Entities.Producto.Crear(
                crearProductodto.Nombre, crearProductodto.Descripcion, crearProductodto.Material,
                crearProductodto.Preciobase, crearProductodto.Urlimagen, crearProductodto.IdCategoria);
            await unitofwork.Productos.AddAsync(producto);
            await unitofwork.SaveAsync();
        }
    }
}