using Domain.Exceptions;

namespace Domain.Entities;

public class CategoriaProducto
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public DateTime? Fechacreacion { get; private set; }
    public DateTime? Fechamodificacion { get; private set; }

    private CategoriaProducto() { }

    public static CategoriaProducto Crear(string nombre, string descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripción es requerida.");

        return new CategoriaProducto
        {
            Nombre = nombre.Trim(),
            Descripcion = descripcion.Trim(),
            Fechacreacion = DateTime.Now,
            Fechamodificacion = DateTime.UtcNow
        };
    }

    public void Actualizar(string nombre, string descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripción es requerida.");

        Nombre = nombre.Trim();
        Descripcion = descripcion.Trim();
        Fechamodificacion = DateTime.UtcNow;
    }
}