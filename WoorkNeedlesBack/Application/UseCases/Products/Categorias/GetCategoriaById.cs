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
    public class GetCategoriaById(ICategoriaProductoRepository repository, IMapper mapper)
    {
        public async Task<CategoriaProductoDto> TraerCategoriaPorId(int id)
        {
            var categoria = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            return mapper.Map<CategoriaProductoDto>(categoria);
        }
    }
}