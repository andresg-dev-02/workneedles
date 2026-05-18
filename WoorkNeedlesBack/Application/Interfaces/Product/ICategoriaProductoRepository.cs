using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Product
{
    public interface ICategoriaProductoRepository
    {
        Task<IEnumerable<CategoriaProducto>> GetAllAsync();
        Task<CategoriaProducto?> GetByIdAsync(int id);
        Task CreateAsync(CategoriaProducto categoria);
        Task UpdateAsync(CategoriaProducto categoria);
        Task DeleteAsync(int id);
    }
}