namespace Application.DTOs.UserModel;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? Fechamodificacion { get; set; }
    public string Rol { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
}

public class CreateUsuarioDto
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public int IdRol { get; set; }
    public int IdPais { get; set; }
    public int IdCiudad { get; set; }
}

public class UpdateUsuarioDto : CreateUsuarioDto
{
    public new string? ContrasenaNueva { get; set; } 
    public bool Activo { get; set; }
}