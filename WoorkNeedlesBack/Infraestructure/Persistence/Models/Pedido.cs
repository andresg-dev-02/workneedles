using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int Idcliente { get; set; }

    public int Idusuario { get; set; }

    public DateTime? Fechapedido { get; set; }

    public DateOnly Fechentregaaprox { get; set; }

    public DateOnly Fechaentrega { get; set; }

    public string Estado { get; set; } = null!;

    public string Direccionentrega { get; set; } = null!;

    public string Observaciones { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal? Descuento { get; set; }

    public decimal Total { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public string? Tokenconfirmacion { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual ICollection<HistorialPedido> HistorialPedidos { get; set; } = new List<HistorialPedido>();

    public virtual Cliente IdclienteNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
