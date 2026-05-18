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
    public class GetCategoriaById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<CategoriaProductoDto> Execute(int id)
        {
            var categoria = await unitofwork.Categorias.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            return mapper.Map<CategoriaProductoDto>(categoria);
        }
    }
}