using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DetallePedido
{
    public int Id { get; set; }

    public int Idpedido { get; set; }

    public int Idproducto { get; set; }

    public string? Talla { get; set; }

    public string? Color { get; set; }

    public int Cantidad { get; set; }

    public decimal Preciounitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Pedido IdpedidoNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
