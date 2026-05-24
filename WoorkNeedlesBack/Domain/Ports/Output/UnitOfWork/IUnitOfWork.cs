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
        IGenericRepository<Domain.Entities.CategoriaInsumo> CategoriasInsumo { get; }
        IGenericRepository<Domain.Entities.Insumo> Insumos { get; }
        IGenericRepository<Domain.Entities.InsumosProducto> InsumosProducto { get; }
        IGenericRepository<Domain.Entities.Cliente> Clientes { get; }
        IGenericRepository<Domain.Entities.Pedido> Pedidos { get; }
        IGenericRepository<Domain.Entities.DetallePedido> DetallesPedido { get; }
        IGenericRepository<Domain.Entities.Pago> Pagos { get; }
        IGenericRepository<Domain.Entities.HistorialPedido> HistorialPedidos { get; }
        IGenericRepository<Domain.Entities.Devolucione> Devoluciones { get; }
        IGenericRepository<Domain.Entities.Ciudade> Ubicaciones { get; }
        Task SaveAsync();
    }
}