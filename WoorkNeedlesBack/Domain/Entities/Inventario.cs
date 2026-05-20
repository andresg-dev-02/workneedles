using Domain.Exceptions;

namespace Domain.Entities;

public class Inventario
{
    public int Id { get; private set; }
    public int? Idproducto { get; private set; }
    public int? Idcolor { get; private set; }
    public int? Idtalla { get; private set; }
    public int Stock { get; private set; }
    public DateTime? Fechacreacion { get; private set; }
    public DateTime? Fechamodificacion { get; private set; }

    public string NombreProducto { get; private set; } = string.Empty;
    public string NombreColor { get; private set; } = string.Empty;
    public string NombreTalla { get; private set; } = string.Empty;

    private Inventario() { }

    public static Inventario Crear(int idProducto, int idColor, int idTalla, int stock)
    {
        if (idProducto <= 0)
            throw new DomainException("El producto es requerido.");
        if (idColor <= 0)
            throw new DomainException("El color es requerido.");
        if (idTalla <= 0)
            throw new DomainException("La talla es requerida.");
        if (stock < 0)
            throw new DomainException("El stock no puede ser negativo.");

        var i = new Inventario { Fechacreacion = DateTime.Now };
        i.Actualizar(idProducto, idColor, idTalla, stock);
        return i;
    }

    public void Actualizar(int idProducto, int idColor, int idTalla, int stock)
    {
        if (stock < 0)
            throw new DomainException("El stock no puede ser negativo.");

        Idproducto = idProducto;
        Idcolor = idColor;
        Idtalla = idTalla;
        Stock = stock;
        Fechamodificacion = DateTime.Now;
    }
}