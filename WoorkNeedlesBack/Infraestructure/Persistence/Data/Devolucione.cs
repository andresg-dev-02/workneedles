using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

public partial class Devolucione
{
    public int Id { get; set; }

    public int Idpedido { get; set; }

    public int Idusuario { get; set; }

    public string Motivo { get; set; } = null!;

    public string? Estado { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Idcliente { get; set; }

    public virtual Cliente? IdclienteNavigation { get; set; }

    public virtual TokenConfirmacion IdpedidoNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
