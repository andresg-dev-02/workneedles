using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Categorias
{
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
}
