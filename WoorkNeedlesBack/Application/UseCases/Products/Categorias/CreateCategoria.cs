using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Application.Interfaces.Product;
using Domain.Entities;
using AutoMapper;

namespace Application.UseCases.Products.Categorias
{
    public class CreateCategoria(ICategoriaProductoRepository repository)
    {
        public async Task CrearCategoriaProducto(CreateCategoriaProductoDto dto)
        {
            var categoria = CategoriaProducto.Crear(dto.Nombre, dto.Descripcion);
            await repository.CreateAsync(categoria);
        }
    }
}
