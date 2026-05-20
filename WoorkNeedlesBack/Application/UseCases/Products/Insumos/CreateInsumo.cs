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
    public class CreateInsumo(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateInsumoDto dto)
        {
            var insumo = Domain.Entities.Insumo.Crear(dto.IdCategoria, dto.Nombre, dto.Descripcion,
                dto.Unidadmedida, dto.Stockactual, dto.Stockalerta, dto.Precio, dto.Proveedor);
            await unitofwork.Insumos.AddAsync(insumo);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllInsumos(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<InsumoDto>> Execute()
        {
            var options = new QueryOptions<Insumo>()
                .AddInclude("IdcategoriaNavigation");
            var insumos = await unitofwork.Insumos.GetAllAsync(options);
            return mapper.Map<IEnumerable<InsumoDto>>(insumos);
        }
    }

    public class GetInsumoById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<InsumoDto> Execute(int id)
        {
            var options = new QueryOptions<Insumo>()
                .AddInclude("IdcategoriaNavigation");
            var insumo = await unitofwork.Insumos.GetByIdAsync(id, options)
                ?? throw new KeyNotFoundException("Insumo no encontrado.");
            return mapper.Map<InsumoDto>(insumo);
        }
    }

    public class UpdateInsumo(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateInsumoDto dto)
        {
            var insumo = await unitofwork.Insumos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Insumo no encontrado.");
            insumo.Actualizar(dto.IdCategoria, dto.Nombre, dto.Descripcion,
                dto.Unidadmedida, dto.Stockactual, dto.Stockalerta, dto.Precio, dto.Proveedor);
            unitofwork.Insumos.Update(insumo);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteInsumo(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var insumo = await unitofwork.Insumos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Insumo no encontrado.");
            unitofwork.Insumos.Delete(insumo);
            await unitofwork.SaveAsync();
        }
    }
}