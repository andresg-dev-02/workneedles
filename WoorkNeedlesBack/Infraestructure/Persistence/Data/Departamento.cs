using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

public partial class Departamento
{
    public int Id { get; set; }

    public int Idpais { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Paise IdpaisNavigation { get; set; } = null!;
}
