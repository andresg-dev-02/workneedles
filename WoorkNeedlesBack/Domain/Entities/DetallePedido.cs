using Domain.Exceptions;

namespace Domain.Entities;

public class DetallePedido
{
    public int Id { get; private set; }
    public int Idpedido { get; private set; }
    public int Idproducto { get; private set; }
    public int? Idinventario { get; private set; }
    public int Cantidad { get; private set; }
    public decimal Preciounitario { get; private set; }
    public decimal Subtotal { get; private set; }
    public string NombreProducto { get; private set; } = string.Empty;
    public string Talla { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;

    private DetallePedido() { }

    public static DetallePedido Crear(int idPedido, int idProducto,
        int? idInventario, int cantidad, decimal precioUnitario)
    {
        if (idPedido <= 0)
            throw new DomainException("El pedido es requerido.");
        if (idProducto <= 0)
            throw new DomainException("El producto es requerido.");
        if (cantidad <= 0)
            throw new DomainException("La cantidad debe ser mayor a 0.");
        if (precioUnitario <= 0)
            throw new DomainException("El precio unitario debe ser mayor a 0.");

        var d = new DetallePedido();
        d.Actualizar(idPedido, idProducto, idInventario, cantidad, precioUnitario);
        return d;
    }

    public void Actualizar(int idPedido, int idProducto,
        int? idInventario, int cantidad, decimal precioUnitario)
    {
        if (cantidad <= 0)
            throw new DomainException("La cantidad debe ser mayor a 0.");
        if (precioUnitario <= 0)
            throw new DomainException("El precio unitario debe ser mayor a 0.");

        Idpedido = idPedido;
        Idproducto = idProducto;
        Idinventario = idInventario;
        Cantidad = cantidad;
        Preciounitario = precioUnitario;
        Subtotal = cantidad * precioUnitario;
    }
}
