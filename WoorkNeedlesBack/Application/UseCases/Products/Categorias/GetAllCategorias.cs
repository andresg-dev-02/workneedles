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
    public class GetAllCategorias(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<CategoriaProductoDto>> Execute()
        {
            var categorias = await unitofwork.Categorias.GetAllAsync();
            return mapper.Map<IEnumerable<CategoriaProductoDto>>(categorias);
        }
    }
}
