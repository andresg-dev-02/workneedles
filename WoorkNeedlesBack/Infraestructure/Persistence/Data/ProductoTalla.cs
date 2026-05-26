using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

public partial class ProductoTalla
{
    public int Id { get; set; }

    public int? Idproducto { get; set; }

    public int? Idtalla { get; set; }

    public DateTime Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual Producto? IdproductoNavigation { get; set; }

    public virtual Talla? IdtallaNavigation { get; set; }
}
