using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class HistorialPedido
{
    public int Id { get; set; }

    public int Idpedido { get; set; }

    public int Idusuario { get; set; }

    public string? Estadoanterior { get; set; }

    public string? Estadoactual { get; set; }

    public string? Observacion { get; set; }

    public DateTime Fecha { get; set; }

    public virtual TokenConfirmacion IdpedidoNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
