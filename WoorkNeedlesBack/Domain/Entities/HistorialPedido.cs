using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class HistorialPedido
    {
        public int Id { get; private set; }
        public int Idpedido { get; private set; }
        public int Idusuario { get; private set; }
        public string? Estadoanterior { get; private set; }
        public string? Estadoactual { get; private set; }
        public string? Observacion { get; private set; }
        public DateTime Fecha { get; private set; }
        public string NombreUsuario { get; private set; } = string.Empty;

        private HistorialPedido() { }

        public static HistorialPedido Crear(int idPedido, int idUsuario,
            string? estadoAnterior, string? estadoActual, string? observacion)
        {
            if (idPedido <= 0)
                throw new DomainException("El pedido es requerido.");
            if (idUsuario <= 0)
                throw new DomainException("El usuario es requerido.");

            return new HistorialPedido
            {
                Idpedido = idPedido,
                Idusuario = idUsuario,
                Estadoanterior = estadoAnterior?.Trim(),
                Estadoactual = estadoActual?.Trim(),
                Observacion = observacion?.Trim(),
                Fecha = DateTime.Now
            };
        }
    }
}