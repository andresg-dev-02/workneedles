using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Pedido
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime? Fechapedido { get; set; }
        public DateOnly Fechentregaaprox { get; set; }
        public DateOnly Fechaentrega { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Direccionentrega { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
    }

    public class CreatePedidoDto
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateOnly FechEntregaAprox { get; set; }
        public DateOnly FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public decimal? Descuento { get; set; }
    }

    public class UpdatePedidoDto : CreatePedidoDto { }

    public class CambiarEstadoPedidoDto
    {
        public string Estado { get; set; } = string.Empty;
    }
}