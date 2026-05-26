using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public int Idpais { get; set; }

    public int Iddepart { get; set; }

    public int Idciudad { get; set; }

    public string Tipocliente { get; set; } = null!;

    public string Tipodocumento { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Razonsocial { get; set; }

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? PreferenciasCompra { get; set; }

    public bool? Activo { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual Ciudade IdciudadNavigation { get; set; } = null!;

    public virtual Departamento IddepartNavigation { get; set; } = null!;

    public virtual Paise IdpaisNavigation { get; set; } = null!;

    public virtual ICollection<TokenConfirmacion> TokenConfirmacions { get; set; } = new List<TokenConfirmacion>();
}
