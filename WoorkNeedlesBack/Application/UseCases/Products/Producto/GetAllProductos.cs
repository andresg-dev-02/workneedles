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
}