using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Product;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.ProductRepo;

    public class CategoriaProductoRepository : ICategoriaProductoRepository
    {
        private readonly WoorkNeedlesContext _context;
        private readonly IMapper _mapper;

        public CategoriaProductoRepository(WoorkNeedlesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaProducto>> GetAllAsync()
        {
            var categorias = await _context.CategoriaProductos.ToListAsync();
            return _mapper.Map<IEnumerable<CategoriaProducto>>(categorias);
        }

        public async Task<CategoriaProducto?> GetByIdAsync(int id)
        {
            var categoria = await _context.CategoriaProductos
                .FirstOrDefaultAsync(c => c.Id == id);
            return categoria is null ? null : _mapper.Map<CategoriaProducto>(categoria);
        }

        public async Task CreateAsync(CategoriaProducto categoria)
        {
            var categoriaNueva = _mapper.Map<Infraestructure.Persistence.Models.CategoriaProducto>(categoria);
            _context.CategoriaProductos.Add(categoriaNueva);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoriaProducto categoria)
        {
            var categoriaExistente = await _context.CategoriaProductos.FindAsync(categoria.Id);
            if (categoriaExistente is null) throw new KeyNotFoundException("Categoría no encontrada.");
            _mapper.Map(categoria, categoriaExistente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria is null) throw new KeyNotFoundException("Categoría no encontrada.");
            _context.CategoriaProductos.Remove(categoria);
            await _context.SaveChangesAsync();
        }

    }
