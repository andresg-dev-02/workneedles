using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Producto
{
    public int Id { get; set; }

    public int Idcategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Material { get; set; }

    public decimal Preciobase { get; set; }

    public string? Urlimagen { get; set; }

    public bool? Activo { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public string? Genero { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual CategoriaProducto IdcategoriaNavigation { get; set; } = null!;

    public virtual ICollection<InsumosProducto> InsumosProductos { get; set; } = new List<InsumosProducto>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<ProductoColore> ProductoColores { get; set; } = new List<ProductoColore>();

    public virtual ICollection<ProductoTalla> ProductoTallas { get; set; } = new List<ProductoTalla>();
}
