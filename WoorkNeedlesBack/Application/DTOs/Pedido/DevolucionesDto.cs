using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Pedido
{
    public class DevolucionesDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string? Estado { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class CreateDevolucioneDto
    {
        public int Idpedido { get; set; }
        public int Idusuario { get; set; }
        public int? Idcliente { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }

    public class CambiarEstadoDevolucioneDto
    {
        public string Estado { get; set; } = string.Empty;
    }
    
}