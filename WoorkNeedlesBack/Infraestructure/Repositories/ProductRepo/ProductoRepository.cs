using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Product;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.ProductRepo
{
    public class ProductoRepository(WoorkNeedlesContext context, IMapper mapper) : IProductoRepository
    {

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            var productos = await context.Productos
                .Include(p => p.IdcategoriaNavigation)
                .ToListAsync();
                
            return mapper.Map<IEnumerable<Producto>>(productos);
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            var model = await context.Productos
                .Include(p => p.IdcategoriaNavigation)
                .FirstOrDefaultAsync(p => p.Id == id);
            return model is null ? null : mapper.Map<Producto>(model);
        }

        public async Task CreateAsync(Producto producto)
        {
            var model = mapper.Map<Infraestructure.Persistence.Models.Producto>(producto);
            context.Productos.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {
            var model = await context.Productos.FindAsync(producto.Id);
            if (model is null) throw new KeyNotFoundException("Producto no encontrado.");
            mapper.Map(producto, model);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await context.Productos.FindAsync(id);
            if (model is null) throw new KeyNotFoundException("Producto no encontrado.");
            context.Productos.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
