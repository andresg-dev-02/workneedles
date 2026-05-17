namespace Application.DTOs;

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
}