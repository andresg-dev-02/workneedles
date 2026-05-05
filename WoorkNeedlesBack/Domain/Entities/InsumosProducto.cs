using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class InsumosProducto
{
    public int Id { get; set; }

    public int Idproducto { get; set; }

    public int Idinsumo { get; set; }

    public decimal Cantidad { get; set; }

    public virtual Insumo IdinsumoNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
