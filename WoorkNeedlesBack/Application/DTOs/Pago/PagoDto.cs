using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Pago
{
    public class PagoDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime? Fechapago { get; set; }
        public decimal Monto { get; set; }
        public string Tipopago { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Observaciones { get; set; }
    }

    public class CreatePagoDto
    {
        public int Idpedido { get; set; }
        public int Idusuario { get; set; }
        public decimal Monto { get; set; }
        public string Tipopago { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Observaciones { get; set; }
    }

    public class CambiarEstadoPagoDto
    {
        public string Estado { get; set; } = string.Empty;
    }
}