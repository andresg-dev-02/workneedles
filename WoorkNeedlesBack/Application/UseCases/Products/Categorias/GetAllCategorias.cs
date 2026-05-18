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

    public class GetAllCategorias(ICategoriaProductoRepository repository, IMapper mapper)
    {
        public async Task<IEnumerable<CategoriaProductoDto>> TraerCategorias()
        {
            var categorias = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<CategoriaProductoDto>>(categorias);
        }
    }  
}
