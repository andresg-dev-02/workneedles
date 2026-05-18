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
    public class CreateCategoria(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateCategoriaProductoDto categoriaProductodto)
        {
            var categoria = CategoriaProducto.Crear(categoriaProductodto.Nombre, categoriaProductodto.Descripcion);
            await unitofwork.Categorias.AddAsync(categoria);
            await unitofwork.SaveAsync();
        }
    }
}
