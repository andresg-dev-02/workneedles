using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Pago
    {
        public int Id { get; private set; }
        public int Idpedido { get; private set; }
        public int Idusuario { get; private set; }
        public DateTime? Fechapago { get; private set; }
        public decimal Monto { get; private set; }
        public string Tipopago { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;
        public string? Referencia { get; private set; }
        public string? Observaciones { get; private set; }
        public DateTime? Fechacreacion { get; private set; }
        public string NombrePedido { get; private set; } = string.Empty;
        public string NombreUsuario { get; private set; } = string.Empty;

        private Pago() { }

        public static Pago Crear(int idPedido, int idUsuario, decimal monto,
            string tipoPago, string? referencia, string? observaciones)
        {
            if (idPedido <= 0)
                throw new DomainException("El pedido es requerido.");
            if (idUsuario <= 0)
                throw new DomainException("El usuario es requerido.");
            if (monto <= 0)
                throw new DomainException("El monto debe ser mayor a 0.");
            if (string.IsNullOrWhiteSpace(tipoPago))
                throw new DomainException("El tipo de pago es requerido.");

            var p = new Pago { Fechapago = DateTime.Now, Fechacreacion = DateTime.Now, Estado = "pendiente" };
            p.Actualizar(idPedido, idUsuario, monto, tipoPago, referencia, observaciones);
            return p;
        }

        public void Actualizar(int idPedido, int idUsuario, decimal monto,
            string tipoPago, string? referencia, string? observaciones)
        {
            if (monto <= 0)
                throw new DomainException("El monto debe ser mayor a 0.");

            Idpedido = idPedido;
            Idusuario = idUsuario;
            Monto = monto;
            Tipopago = tipoPago.Trim();
            Referencia = referencia?.Trim();
            Observaciones = observaciones?.Trim();
        }

        public void CambiarEstado(string nuevoEstado)
        {
            var estadosValidos = new[] { "pendiente", "completado", "reembolsado", "fallido" };
            if (!estadosValidos.Contains(nuevoEstado))
                throw new DomainException("Estado de pago no válido.");
            Estado = nuevoEstado;
        }
    }
}