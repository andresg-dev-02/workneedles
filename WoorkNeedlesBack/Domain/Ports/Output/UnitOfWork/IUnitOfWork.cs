using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output;
using Domain.Entities;

namespace Domain.Ports.Output.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Domain.Entities.Usuario> Usuarios { get; }
        IGenericRepository<Domain.Entities.CategoriaProducto> Categorias { get; }
        IGenericRepository<Domain.Entities.Producto> Productos { get; }
        Task SaveAsync();
    }
}