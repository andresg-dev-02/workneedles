using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Ciudade
{
    public int Id { get; set; }

    public int Iddepart { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Departamento IddepartNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
