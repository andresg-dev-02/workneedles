using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Pedido
{
    public class DetallePedidoDto
    {
        public int Id { get; set; }
        public int? Idinventario { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string Talla { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Preciounitario { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }

    public class CreateDetallePedidoDto
    {
        public int Idpedido { get; set; }
        public int Idproducto { get; set; }
        public int? Idinventario { get; set; }
        public int Cantidad { get; set; }
        public decimal Preciounitario { get; set; }
    }

    public class UpdateDetallePedidoDto
    {
        public int? Idinventario { get; set; }
        public int Cantidad { get; set; }
        public decimal Preciounitario { get; set; }
    }
}