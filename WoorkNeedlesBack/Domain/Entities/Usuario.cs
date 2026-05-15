using Domain.Exceptions;

namespace Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public int Idrol { get; private set; }
    public int Idpais { get; private set; }
    public int Idciudad { get; private set; }
    public string Nombres { get; private set; }
    public string Apellidos { get; private set; }
    public string Email { get; private set; }
    public string Contrasena { get; private set; }  
    public string Telefono { get; private set; }
    public bool Activo { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime? Fechamodificacion { get; private set; }

#pragma warning disable CS8618
    private Usuario() { }
#pragma warning restore CS8618

    public static Usuario Crear(
        
        string nombres, string apellidos, string email,
        string contrasenaHash, string telefono,
        int rolId, int paisId, int ciudadId)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email es requerido.");
        if (!email.Contains('@'))
            throw new DomainException("El email no es válido.");
        if (string.IsNullOrWhiteSpace(contrasenaHash))
            throw new DomainException("La contraseña es requerida.");

        return new Usuario
        {
            Nombres = nombres.Trim(),
            Apellidos = apellidos.Trim(),
            Email = email.ToLowerInvariant(),
            Contrasena = contrasenaHash,
            Telefono = telefono,
            Idrol = rolId,
            Idpais = paisId,
            Idciudad = ciudadId,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void Activar()
    {
        if (Activo) throw new DomainException("El usuario ya está activo.");
        Activo = true;
        Fechamodificacion = DateTime.UtcNow;
    }
}
