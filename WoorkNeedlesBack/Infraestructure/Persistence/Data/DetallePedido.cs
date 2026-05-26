using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

public partial class DetallePedido
{
    public int Id { get; set; }

    public int Idpedido { get; set; }

    public int Idproducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Preciounitario { get; set; }

    public decimal Subtotal { get; set; }

    public int? Idinventario { get; set; }

    public virtual Inventario? IdinventarioNavigation { get; set; }

    public virtual TokenConfirmacion IdpedidoNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
