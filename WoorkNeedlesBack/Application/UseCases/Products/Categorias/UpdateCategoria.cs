using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Categorias
{
    public class UpdateCategoria(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateCategoriaProductoDto categoriaProductodto)
        {
            var categoria = await unitofwork.Categorias.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            categoria.Actualizar(categoriaProductodto.Nombre, categoriaProductodto.Descripcion);
            unitofwork.Categorias.Update(categoria);
            await unitofwork.SaveAsync();
        }
    }
}