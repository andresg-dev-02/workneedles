using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Reportes
{

    public class ProductoMasVendidoDto
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int TotalVendido { get; set; }
        public decimal TotalIngresos { get; set; }
    }

    public class IngresoMensualDto
    {
        public string Mes { get; set; } = string.Empty;
        public int Anio { get; set; }
        public decimal TotalIngresos { get; set; }
        public int TotalPedidos { get; set; }
    }

    public class FrecuenciaPedidoDto
    {
        public string NombreCliente { get; set; } = string.Empty;
        public int TotalPedidos { get; set; }
        public decimal TotalGastado { get; set; }
    }

    public class ComportamientoClienteDto
    {
        public string NombreCliente { get; set; } = string.Empty;
        public int TotalPedidos { get; set; }
        public decimal TotalGastado { get; set; }
        public DateTime? UltimoPedido { get; set; }
        public string ProductoFavorito { get; set; } = string.Empty;
    }
}