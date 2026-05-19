using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Talla
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual ICollection<ProductoTalla> ProductoTallas { get; set; } = new List<ProductoTalla>();
}
