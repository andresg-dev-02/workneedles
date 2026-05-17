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
