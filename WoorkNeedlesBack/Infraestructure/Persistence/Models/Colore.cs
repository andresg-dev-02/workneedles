using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Colore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigohex { get; set; } = null!;

    public DateTime Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<ProductoColore> ProductoColores { get; set; } = new List<ProductoColore>();
}
