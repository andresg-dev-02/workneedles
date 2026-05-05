using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Insumo
{
    public int Id { get; set; }

    public int Idcategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Unidadmedida { get; set; } = null!;

    public decimal Stockactual { get; set; }

    public decimal Stockalerta { get; set; }

    public decimal Precio { get; set; }

    public string? Proveedor { get; set; }

    public bool? Activo { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual CategoriaInsumo IdcategoriaNavigation { get; set; } = null!;

    public virtual ICollection<InsumosProducto> InsumosProductos { get; set; } = new List<InsumosProducto>();
}
