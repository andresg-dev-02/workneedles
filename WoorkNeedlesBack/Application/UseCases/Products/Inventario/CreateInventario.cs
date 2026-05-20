using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Products.Inventario
{
    public class CreateInventario(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateInventarioDto dto)
        {
            var inventario = Domain.Entities.Inventario.Crear(
                dto.Idproducto, dto.Idcolor, dto.Idtalla, dto.Stock);
            await unitofwork.Inventario.AddAsync(inventario);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllInventario(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<InventarioDto>> Execute(int idProducto)
        {
            var options = new QueryOptions<Domain.Entities.Inventario>()
                .AddInclude("IdproductoNavigation")
                .AddInclude("IdcolorNavigation")
                .AddInclude("IdtallaNavigation");

            var inventario = await unitofwork.Inventario.GetAllAsync(options);
            return mapper.Map<IEnumerable<InventarioDto>>(
                inventario.Where(i => i.Idproducto == idProducto));
        }
    }

    public class UpdateInventario(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateInventarioDto dto)
        {
            var inventario = await unitofwork.Inventario.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Inventario no encontrado.");
            inventario.Actualizar(
                inventario.Idproducto!.Value,
                inventario.Idcolor!.Value,
                inventario.Idtalla!.Value,
                dto.Stock);
            unitofwork.Inventario.Update(inventario);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteInventario(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var inventario = await unitofwork.Inventario.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Inventario no encontrado.");
            unitofwork.Inventario.Delete(inventario);
            await unitofwork.SaveAsync();
        }
    }
}