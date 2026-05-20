using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using AutoMapper;
using Domain.Specification;

namespace Application.UseCases.Products.Insumos
{
    public class CreateInsumoProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateInsumosProductoDto dto)
        {
            var insumosProducto = Domain.Entities.InsumosProducto.Crear(dto.Idproducto, dto.Idinsumo, dto.Cantidad);
            await unitofwork.InsumosProducto.AddAsync(insumosProducto);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllInsumosProducto(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<InsumosProductoDto>> Execute(int idProducto)
        {
            var options = new QueryOptions<InsumosProducto>()
                .AddInclude("IdproductoNavigation")
                .AddInclude("IdinsumoNavigation");
            var insumosProducto = await unitofwork.InsumosProducto.GetAllAsync(options);
            return mapper.Map<IEnumerable<InsumosProductoDto>>(
                insumosProducto.Where(ip => ip.Idproducto == idProducto));
        }
    }

    public class UpdateInsumosProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateInsumosProductoDto dto)
        {
            var insumosProducto = await unitofwork.InsumosProducto.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Insumo de producto no encontrado.");
            insumosProducto.Actualizar(
                insumosProducto.Idproducto,
                insumosProducto.Idinsumo,
                dto.Cantidad);
            unitofwork.InsumosProducto.Update(insumosProducto);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteInsumosProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var insumosProducto = await unitofwork.InsumosProducto.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Insumo de producto no encontrado.");
            unitofwork.InsumosProducto.Delete(insumosProducto);
            await unitofwork.SaveAsync();
        }
    }
}