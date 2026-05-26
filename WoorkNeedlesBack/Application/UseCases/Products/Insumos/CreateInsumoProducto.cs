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
            var insumo = await unitofwork.Insumos.GetByIdAsync(dto.Idinsumo)
                ?? throw new KeyNotFoundException("Insumo no encontrado.");

            if (insumo.Stockactual < dto.Cantidad)
                throw new DomainException("Stock insuficiente para este insumo.");

            insumo.Actualizar(insumo.Idcategoria, insumo.Nombre, insumo.Descripcion,
                insumo.Unidadmedida, insumo.Stockactual - dto.Cantidad,
                insumo.Stockalerta, insumo.Precio, insumo.Proveedor);

            var insumosProducto = InsumosProducto.Crear(dto.Idproducto, dto.Idinsumo, dto.Cantidad);
            await unitofwork.InsumosProducto.AddAsync(insumosProducto);
            unitofwork.Insumos.Update(insumo);
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

            var insumo = await unitofwork.Insumos.GetByIdAsync(insumosProducto.Idinsumo)
                ?? throw new KeyNotFoundException("Insumo no encontrado.");

            var diferencia = dto.Cantidad - insumosProducto.Cantidad;

            if (diferencia > 0 && insumo.Stockactual < diferencia)
                throw new DomainException("Stock insuficiente para este insumo.");

            insumo.Actualizar(insumo.Idcategoria, insumo.Nombre, insumo.Descripcion,
                insumo.Unidadmedida, insumo.Stockactual - diferencia,
                insumo.Stockalerta, insumo.Precio, insumo.Proveedor);

            insumosProducto.Actualizar(
                insumosProducto.Idproducto,
                insumosProducto.Idinsumo,
                dto.Cantidad);

            unitofwork.InsumosProducto.Update(insumosProducto);
            unitofwork.Insumos.Update(insumo);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteInsumosProducto(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var insumosProducto = await unitofwork.InsumosProducto.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Insumo de producto no encontrado.");

            var insumo = await unitofwork.Insumos.GetByIdAsync(insumosProducto.Idinsumo)
                ?? throw new KeyNotFoundException("Insumo no encontrado.");

            insumo.Actualizar(insumo.Idcategoria, insumo.Nombre, insumo.Descripcion,
                insumo.Unidadmedida, insumo.Stockactual + insumosProducto.Cantidad,
                insumo.Stockalerta, insumo.Precio, insumo.Proveedor);

            unitofwork.InsumosProducto.Delete(insumosProducto);
            unitofwork.Insumos.Update(insumo);
            await unitofwork.SaveAsync();
        }
    }
}