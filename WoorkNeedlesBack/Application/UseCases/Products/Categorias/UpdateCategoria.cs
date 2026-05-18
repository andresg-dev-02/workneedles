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
    public class UpdateCategoria(ICategoriaProductoRepository repository)
    { 
        public async Task ActualizarCategoriaProducto(int id, UpdateCategoriaProductoDto dto)
        {
            var categoria = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            categoria.Actualizar(dto.Nombre, dto.Descripcion);
            await repository.UpdateAsync(categoria);
        }
    }
}