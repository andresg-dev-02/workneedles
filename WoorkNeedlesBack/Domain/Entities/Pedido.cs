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

        private Pedido() { }

        public static Pedido Crear(int idCliente, int idUsuario,
        DateOnly fechEntregaAprox, DateOnly fechaEntrega,
        string direccionEntrega, string observaciones,
        decimal subtotal, decimal? descuento)
        {
            if (idCliente <= 0)
                throw new DomainException("El cliente es requerido.");
            if (idUsuario <= 0)
                throw new DomainException("El usuario es requerido.");
            if (string.IsNullOrWhiteSpace(direccionEntrega))
                throw new DomainException("La dirección de entrega es requerida.");
            if (subtotal <= 0)
                throw new DomainException("El subtotal debe ser mayor a 0.");

            var p = new Pedido
            {
                Fechapedido = DateTime.Now,
                Fechacreacion = DateTime.Now,
                Estado = "pendiente"
            };
            p.Actualizar(idCliente, idUsuario, fechEntregaAprox, fechaEntrega,
                direccionEntrega, observaciones, subtotal, descuento);
            return p;
        }

        public void Actualizar(int idCliente, int idUsuario,
            DateOnly fechEntregaAprox, DateOnly fechaEntrega,
            string direccionEntrega, string observaciones,
            decimal subtotal, decimal? descuento)
        {
            if (string.IsNullOrWhiteSpace(direccionEntrega))
                throw new DomainException("La dirección de entrega es requerida.");
            if (subtotal <= 0)
                throw new DomainException("El subtotal debe ser mayor a 0.");

            Idcliente = idCliente;
            Idusuario = idUsuario;
            Fechentregaaprox = fechEntregaAprox;
            Fechaentrega = fechaEntrega;
            Direccionentrega = direccionEntrega.Trim();
            Observaciones = observaciones.Trim();
            Subtotal = subtotal;
            Descuento = descuento;
            Total = subtotal - (descuento ?? 0);
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
    }
}