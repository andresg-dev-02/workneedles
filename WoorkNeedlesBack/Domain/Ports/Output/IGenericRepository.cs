using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Specification;

namespace Domain.Ports.Output
{
    public interface IGenericRepository<TDomain> where TDomain : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync(QueryOptions<TDomain>? options = null);
        Task<TDomain?> GetByIdAsync(int id, QueryOptions<TDomain>? options = null);
        Task AddAsync(TDomain entity);
        void Update(TDomain entity);
        void Delete(TDomain entity);
    }
}