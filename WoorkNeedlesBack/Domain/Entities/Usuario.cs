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
    public string Rol { get; private set; } = string.Empty;
    public string Pais { get; private set; } = string.Empty;
    public string Ciudad { get; private set; } = string.Empty;

#pragma warning disable CS8618
    private Usuario() { }
#pragma warning restore CS8618

    public static Usuario Crear(string nombres, string apellidos, string email,
        string contrasenaHash, string telefono, int rolId, int paisId, int ciudadId)
    {
        if (string.IsNullOrWhiteSpace(contrasenaHash))
            throw new DomainException("La contraseña es requerida.");

        var usuario = new Usuario
        {
            Activo = true,
            FechaCreacion = DateTime.Now,
            Contrasena = contrasenaHash
        };

        usuario.Actualizar(nombres, apellidos, email, telefono, rolId, paisId, ciudadId);

        return usuario;
    }

    public void Activar()
    {
        if (Activo) throw new DomainException("El usuario ya está activo.");
        Activo = true;
        Fechamodificacion = DateTime.Now;
    }

    public void Actualizar(string nombres, string apellidos, string email,
        string telefono, int idRol, int idPais, int idCiudad, string? nuevaContrasena = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email es requerido.");
        if (!email.Contains('@'))
            throw new DomainException("El email no es válido.");

        Nombres = nombres.Trim();
        Apellidos = apellidos.Trim();
        Email = email.ToLowerInvariant();
        Telefono = telefono;
        Idrol = idRol;
        Idpais = idPais;
        Idciudad = idCiudad;

        if (!string.IsNullOrWhiteSpace(nuevaContrasena))
            Contrasena = nuevaContrasena;

        Fechamodificacion = DateTime.Now;
    }
}
