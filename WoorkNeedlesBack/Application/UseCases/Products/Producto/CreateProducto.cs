using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Products.Producto
{
    public class CreateProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateProductoDto crearProductodto)
        {
            var producto = Domain.Entities.Producto.Crear(
                crearProductodto.Nombre, crearProductodto.Descripcion, crearProductodto.Material,
                crearProductodto.Preciobase, crearProductodto.Urlimagen, crearProductodto.IdCategoria, crearProductodto.Genero);
            await unitofwork.Productos.AddAsync(producto);
            await unitofwork.SaveAsync();
        }
    }

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

    public class GetAllProductos(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoDto>> Execute()
        {
            var options = new QueryOptions<Domain.Entities.Producto>()
                .AddInclude("IdcategoriaNavigation");
            var productos = await unitofwork.Productos.GetAllAsync(options);
            return mapper.Map<IEnumerable<ProductoDto>>(productos);
        }
    }

    public class GetProductoById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<ProductoDto> Execute(int id)
        {
            var options = new QueryOptions<Domain.Entities.Producto>()
                .AddInclude("IdcategoriaNavigation");

            var producto = await unitofwork.Productos.GetByIdAsync(id, options)
                ?? throw new KeyNotFoundException("Producto no encontrado.");

            return mapper.Map<ProductoDto>(producto);
        }
    }

    public class UpdateProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateProductoDto actualizarProductodto)
        {
            var producto = await unitofwork.Productos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Producto no encontrado.");
            producto.Actualizar(
                actualizarProductodto.Nombre, actualizarProductodto.Descripcion, actualizarProductodto.Material,
                actualizarProductodto.Preciobase, actualizarProductodto.Urlimagen, actualizarProductodto.IdCategoria, actualizarProductodto.Genero, actualizarProductodto.Activo);
            unitofwork.Productos.Update(producto);
            await unitofwork.SaveAsync();
        }
    }
}