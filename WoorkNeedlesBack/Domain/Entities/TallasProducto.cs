using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TallasProducto
{
    public int Id { get; set; }

    public int Idproducto { get; set; }

    public string Talla { get; set; } = null!;

    public int Stock { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
