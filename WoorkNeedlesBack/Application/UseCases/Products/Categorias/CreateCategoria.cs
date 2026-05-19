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
    public class CreateCategoria(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateCategoriaProductoDto categoriaProductodto)
        {
            var categoria = CategoriaProducto.Crear(categoriaProductodto.Nombre, categoriaProductodto.Descripcion);
            await unitofwork.Categorias.AddAsync(categoria);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteCategoria(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var categoria = await unitofwork.Categorias.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            unitofwork.Categorias.Delete(categoria);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllCategorias(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<CategoriaProductoDto>> Execute()
        {
            var categorias = await unitofwork.Categorias.GetAllAsync();
            return mapper.Map<IEnumerable<CategoriaProductoDto>>(categorias);
        }
    }

    public class GetCategoriaById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<CategoriaProductoDto> Execute(int id)
        {
            var categoria = await unitofwork.Categorias.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            return mapper.Map<CategoriaProductoDto>(categoria);
        }
    }

    public class UpdateCategoria(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateCategoriaProductoDto categoriaProductodto)
        {
            var categoria = await unitofwork.Categorias.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoría no encontrada.");
            categoria.Actualizar(categoriaProductodto.Nombre, categoriaProductodto.Descripcion);
            unitofwork.Categorias.Update(categoria);
            await unitofwork.SaveAsync();
        }
    }
}
