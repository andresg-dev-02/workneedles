using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Products.Producto
{
    public class FiltrarProductos(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoDto>> Execute(FiltroProductoDto filtro)
        {
            var options = new QueryOptions<Domain.Entities.Producto>()
                .AddInclude("IdcategoriaNavigation");

            var productos = await unitofwork.Productos.GetAllAsync(options);

            var query = productos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                query = query.Where(p => p.Nombre.Contains(filtro.Nombre, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filtro.Categoria))
                query = query.Where(p => p.Categoria.Contains(filtro.Categoria, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filtro.Material))
                query = query.Where(p => p.Material != null && p.Material.Contains(filtro.Material, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filtro.Genero))
                query = query.Where(p => p.Genero != null && p.Genero.Equals(filtro.Genero, StringComparison.OrdinalIgnoreCase));

            if (filtro.PrecioMin.HasValue)
                query = query.Where(p => p.Preciobase >= filtro.PrecioMin.Value);

            if (filtro.PrecioMax.HasValue)
                query = query.Where(p => p.Preciobase <= filtro.PrecioMax.Value);

            if (filtro.Disponible.HasValue && filtro.Disponible.Value)
                query = query.Where(p => p.Activo);

            var productosFiltrados = query.ToList();


            if (!string.IsNullOrWhiteSpace(filtro.Talla) || !string.IsNullOrWhiteSpace(filtro.Color))
            {
                var inventarioOptions = new QueryOptions<Domain.Entities.Inventario>()
                    .AddInclude("IdtallaNavigation")
                    .AddInclude("IdcolorNavigation");

                var inventario = await unitofwork.Inventario.GetAllAsync(inventarioOptions);

                if (!string.IsNullOrWhiteSpace(filtro.Talla))
                {
                    var idsConTalla = inventario
                        .Where(i => i.NombreTalla.Equals(filtro.Talla, StringComparison.OrdinalIgnoreCase)
                                    && i.Stock > 0)
                        .Select(i => i.Idproducto)
                        .Distinct()
                        .ToList();
                    productosFiltrados = productosFiltrados
                        .Where(p => idsConTalla.Contains(p.Id))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(filtro.Color))
                {
                    var idsConColor = inventario
                        .Where(i => i.NombreColor.Equals(filtro.Color, StringComparison.OrdinalIgnoreCase)
                                    && i.Stock > 0)
                        .Select(i => i.Idproducto)
                        .Distinct()
                        .ToList();
                    productosFiltrados = productosFiltrados
                        .Where(p => idsConColor.Contains(p.Id))
                        .ToList();
                }

                if (filtro.Disponible.HasValue && filtro.Disponible.Value)
                {
                    var idsConStock = inventario
                        .Where(i => i.Stock > 0)
                        .Select(i => i.Idproducto)
                        .Distinct()
                        .ToList();
                    productosFiltrados = productosFiltrados
                        .Where(p => idsConStock.Contains(p.Id))
                        .ToList();
                }
            }

            return mapper.Map<IEnumerable<ProductoDto>>(productosFiltrados);
        }
    }
}