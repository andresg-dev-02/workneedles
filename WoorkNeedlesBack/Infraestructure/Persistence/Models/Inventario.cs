using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public int? Idproducto { get; set; }

    public int? Idcolor { get; set; }

    public int? Idtalla { get; set; }

    public int Stock { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual Colore? IdcolorNavigation { get; set; }

    public virtual Producto? IdproductoNavigation { get; set; }

    public virtual Talla? IdtallaNavigation { get; set; }
}
