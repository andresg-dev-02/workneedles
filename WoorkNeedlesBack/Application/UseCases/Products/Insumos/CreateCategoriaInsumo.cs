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
    public class CreateCategoriaInsumo(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateCategoriaInsumoDto dto)
        {
            var categoria = Domain.Entities.CategoriaInsumo.Crear(dto.Nombre, dto.Descripcion);
            await unitofwork.CategoriasInsumo.AddAsync(categoria);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllCategoriasInsumo(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<CategoriaInsumoDto>> Execute()
        {
            var categorias = await unitofwork.CategoriasInsumo.GetAllAsync();
            return mapper.Map<IEnumerable<CategoriaInsumoDto>>(categorias);
        }
    }

    public class GetCategoriaInsumoById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<CategoriaInsumoDto> Execute(int id)
        {
            var categoria = await unitofwork.CategoriasInsumo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría de insumo no encontrada.");
            return mapper.Map<CategoriaInsumoDto>(categoria);
        }
    }

    public class UpdateCategoriaInsumo(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateCategoriaInsumoDto dto)
        {
            var categoria = await unitofwork.CategoriasInsumo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría de insumo no encontrada.");
            categoria.Actualizar(dto.Nombre, dto.Descripcion);
            unitofwork.CategoriasInsumo.Update(categoria);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteCategoriaInsumo(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var categoria = await unitofwork.CategoriasInsumo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría de insumo no encontrada.");
            unitofwork.CategoriasInsumo.Delete(categoria);
            await unitofwork.SaveAsync();
        }
    }
}