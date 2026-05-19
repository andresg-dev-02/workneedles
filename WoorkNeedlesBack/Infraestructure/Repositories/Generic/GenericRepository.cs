using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Specification;
using AutoMapper;
using Domain.Ports.Output;

namespace Infraestructure.Repositories.Generic
{
    public class GenericRepository<TDomain, TModel>(WoorkNeedlesContext context, IMapper mapper) : IGenericRepository<TDomain> 
        where TDomain : class
        where TModel : class
    {
        protected readonly WoorkNeedlesContext _context = context;
        protected readonly DbSet<TModel> _dbcontextSet = context.Set<TModel>();

        public virtual async Task<IEnumerable<TDomain>> GetAllAsync(QueryOptions<TDomain>? options = null)
        {
            IQueryable<TModel> query = _dbcontextSet.AsNoTracking();
            if (options != null)
                foreach (var include in options.Includes)
                    query = query.Include(include);
            var models = await query.ToListAsync();
            return mapper.Map<IEnumerable<TDomain>>(models);
        }

        public virtual async Task<TDomain?> GetByIdAsync(int id, QueryOptions<TDomain>? options = null)
        {
            IQueryable<TModel> query = _dbcontextSet.AsNoTracking();
            if (options != null)
                foreach (var include in options.Includes)
                    query = query.Include(include);
            var model = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return model is null ? null : mapper.Map<TDomain>(model);
        }

        public async Task AddAsync(TDomain entity)
        {
            var model = mapper.Map<TModel>(entity);
            await _dbcontextSet.AddAsync(model);
        }

        public void Update(TDomain entity)
        {
            var model = mapper.Map<TModel>(entity);
            _dbcontextSet.Update(model);
        }

        public void Delete(TDomain entity)
        {
            var model = mapper.Map<TModel>(entity);
            _dbcontextSet.Remove(model);
        }
    }
}