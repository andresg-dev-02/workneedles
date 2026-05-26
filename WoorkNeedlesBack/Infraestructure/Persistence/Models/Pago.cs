using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int Idpedido { get; set; }

    public int Idusuario { get; set; }

    public DateTime? Fechapago { get; set; }

    public decimal Monto { get; set; }

    public string Tipopago { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Referencia { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public virtual TokenConfirmacion IdpedidoNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
