using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Pedido
    {
        public int Id { get; private set; }
        public int Idcliente { get; private set; }
        public int Idusuario { get; private set; }
        public DateTime? Fechapedido { get; private set; }
        public DateOnly Fechentregaaprox { get; private set; }
        public DateOnly Fechaentrega { get; private set; }
        public string Estado { get; private set; } = string.Empty;
        public string Direccionentrega { get; private set; } = string.Empty;
        public string Observaciones { get; private set; } = string.Empty;
        public decimal Subtotal { get; private set; }
        public decimal? Descuento { get; private set; }
        public decimal Total { get; private set; }
        public DateTime? Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }
        public string NombreCliente { get; private set; } = string.Empty;
        public string NombreUsuario { get; private set; } = string.Empty;
        public string? Tokenconfirmacion  { get; private set; }

        private Pedido() { }

        public static Pedido Crear(int idCliente, int idUsuario,
            DateOnly fechEntregaAprox, DateOnly fechaEntrega,
            string direccionEntrega, string observaciones, decimal? descuento)
        {
            if (idCliente <= 0)
                throw new DomainException("El cliente es requerido.");
            if (idUsuario <= 0)
                throw new DomainException("El usuario es requerido.");
            if (string.IsNullOrWhiteSpace(direccionEntrega))
                throw new DomainException("La dirección de entrega es requerida.");

            var p = new Pedido
            {
                Fechapedido = DateTime.Now,
                Fechacreacion = DateTime.Now,
                Estado = "pendiente",
                Subtotal = 0,
                Total = 0
            };
            p.Actualizar(idCliente, idUsuario, fechEntregaAprox, fechaEntrega,
                direccionEntrega, observaciones, descuento);
            return p;
        }

        public void Actualizar(int idCliente, int idUsuario,
            DateOnly fechEntregaAprox, DateOnly fechaEntrega,
            string direccionEntrega, string observaciones, decimal? descuento)
        {
            if (string.IsNullOrWhiteSpace(direccionEntrega))
                throw new DomainException("La dirección de entrega es requerida.");

            Idcliente = idCliente;
            Idusuario = idUsuario;
            Fechentregaaprox = fechEntregaAprox;
            Fechaentrega = fechaEntrega;
            Direccionentrega = direccionEntrega.Trim();
            Observaciones = observaciones.Trim();
            Descuento = descuento;
            Total = Subtotal - (descuento ?? 0);
            Fechamodificacion = DateTime.Now;
        }

        public void RecalcularTotales(decimal subtotal)
        {
            Subtotal = subtotal;
            Total = subtotal - (Descuento ?? 0);
            Fechamodificacion = DateTime.Now;
        }

        public void CambiarEstado(string nuevoEstado)
        {
            var estadosValidos = new[] { "pendiente", "en preparacion", "enviado", "entregado", "cancelado" };
            if (!estadosValidos.Contains(nuevoEstado))
                throw new DomainException("Estado no válido.");
            Estado = nuevoEstado;
            Fechamodificacion = DateTime.Now;
        }

        public void AsignarToken(string token)
        {
            Tokenconfirmacion  = token;
            Fechamodificacion = DateTime.Now;
        }

        public void LimpiarToken()
        {
            Tokenconfirmacion  = null;
            Fechamodificacion = DateTime.Now;
        }
    }
}