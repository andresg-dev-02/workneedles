using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Paise
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
