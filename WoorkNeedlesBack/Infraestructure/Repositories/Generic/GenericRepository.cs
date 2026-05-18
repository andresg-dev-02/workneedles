using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Specification;

namespace Infraestructure.Repositories.Generic
{
    public class GenericRepository<T>(WoorkNeedlesContext context) : IGenericRepository<T> where T : class
    {
        protected readonly WoorkNeedlesContext _context = context;
        protected readonly DbSet<T> _dbcontextSet = context.Set<T>();

        public virtual async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T>? options = null)
        {
            IQueryable<T> query = _dbcontextSet.AsNoTracking();
            if (options != null)
                foreach (var include in options.Includes)
                    query = query.Include(include); 
            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, QueryOptions<T>? options = null)
        {
            IQueryable<T> query = _dbcontextSet.AsNoTracking();
            if (options != null)
                foreach (var include in options.Includes)
                    query = query.Include(include);
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity) =>
            await _dbcontextSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbcontextSet.Update(entity);

        public void Delete(T entity) =>
            _dbcontextSet.Remove(entity);
    }
}