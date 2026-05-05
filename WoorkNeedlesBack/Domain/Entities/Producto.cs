using System;
using System.Collections.Generic;

namespace Domain.Entities;

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

    public virtual ICollection<ColoresProducto> ColoresProductos { get; set; } = new List<ColoresProducto>();

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual CategoriaProducto IdcategoriaNavigation { get; set; } = null!;

    public virtual ICollection<InsumosProducto> InsumosProductos { get; set; } = new List<InsumosProducto>();

    public virtual ICollection<TallasProducto> TallasProductos { get; set; } = new List<TallasProducto>();
}
