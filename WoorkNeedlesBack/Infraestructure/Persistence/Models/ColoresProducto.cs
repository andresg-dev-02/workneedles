using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class ColoresProducto
{
    public int Id { get; set; }

    public int Idproducto { get; set; }

    public string Color { get; set; } = null!;

    public string? Codigohex { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
