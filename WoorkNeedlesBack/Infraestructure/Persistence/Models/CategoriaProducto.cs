using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class CategoriaProducto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
