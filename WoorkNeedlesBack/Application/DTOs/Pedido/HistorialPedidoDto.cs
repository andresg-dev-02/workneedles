using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Pedido
{
    public class HistorialPedidoDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string? Estadoanterior { get; set; }
        public string? Estadoactual { get; set; }
        public string? Observacion { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CreateHistorialPedidoDto
    {
        public int Idpedido { get; set; }
        public int Idusuario { get; set; }
        public string? Estadoanterior { get; set; }
        public string? Estadoactual { get; set; }
        public string? Observacion { get; set; }
    }
}