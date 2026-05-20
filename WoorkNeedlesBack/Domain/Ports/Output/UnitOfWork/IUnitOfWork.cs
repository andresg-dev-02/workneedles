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
        IGenericRepository<Domain.Entities.Colore> Colores { get; }
        IGenericRepository<Domain.Entities.Talla> Tallas { get; }
        IGenericRepository<Domain.Entities.ProductoColore> ProductoColores { get; }
        IGenericRepository<Domain.Entities.ProductoTalla> ProductoTallas { get; }
        IGenericRepository<Domain.Entities.Inventario> Inventario { get; }
        Task SaveAsync();
    }
}