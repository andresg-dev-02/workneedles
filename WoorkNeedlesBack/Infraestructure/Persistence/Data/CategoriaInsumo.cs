using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

public partial class CategoriaInsumo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();
}
