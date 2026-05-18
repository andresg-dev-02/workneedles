using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Specification;

namespace Domain.Ports.Output
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(QueryOptions<T>? options = null);
        Task<T?> GetByIdAsync(int id, QueryOptions<T>? options = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}