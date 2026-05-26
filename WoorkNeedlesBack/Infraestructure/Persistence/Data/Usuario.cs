using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

public partial class Usuario
{
    public int Id { get; set; }

    public int Idrol { get; set; }

    public int Idpais { get; set; }

    public int Idciudad { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual ICollection<HistorialPedido> HistorialPedidos { get; set; } = new List<HistorialPedido>();

    public virtual Ciudade IdciudadNavigation { get; set; } = null!;

    public virtual Paise IdpaisNavigation { get; set; } = null!;

    public virtual Role IdrolNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<TokenConfirmacion> TokenConfirmacions { get; set; } = new List<TokenConfirmacion>();
}
