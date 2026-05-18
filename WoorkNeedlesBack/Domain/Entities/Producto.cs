using Domain.Exceptions;

namespace Domain.Entities;

public class Producto
{
    public int Id { get; private set; }
    public int Idcategoria { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public string? Material { get; private set; }
    public decimal Preciobase { get; private set; }
    public string? Urlimagen { get; private set; }
    public bool Activo { get; private set; }
    public DateTime? Fechacreacion { get; private set; }
    public DateTime? Fechamodificacion { get; private set; }
    public string Categoria { get; private set; } = string.Empty;
    
    private Producto() { }

    public static Producto Crear(string nombre, string descripcion, string? material,
        decimal preciobase, string? urlimagen, int idcategoria)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripción es requerida.");
        if (preciobase <= 0)
            throw new DomainException("El precio base debe ser mayor a 0.");

        var producto = new Producto
        {
            Activo = true,
            Fechacreacion = DateTime.Now,
            Idcategoria = idcategoria
        };

        producto.Actualizar(nombre, descripcion, material, preciobase, urlimagen, idcategoria);
        return producto;
    }

    public void Actualizar(string nombre, string descripcion, string? material,
        decimal preciobase, string? urlimagen, int idcategoria)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripción es requerida.");
        if (preciobase <= 0)
            throw new DomainException("El precio base debe ser mayor a 0.");

        Nombre = nombre.Trim();
        Descripcion = descripcion.Trim();
        Material = material?.Trim();
        Preciobase = preciobase;
        Urlimagen = urlimagen?.Trim();
        Idcategoria = idcategoria;
        Fechamodificacion = DateTime.Now;
    }
}