using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Devolucione
    {
        public int Id { get; private set; }
        public int Idpedido { get; private set; }
        public int Idusuario { get; private set; }
        public int? Idcliente { get; private set; }
        public string Motivo { get; private set; } = string.Empty;
        public string? Estado { get; private set; }
        public DateTime? Fecha { get; private set; }
        public string NombreUsuario { get; private set; } = string.Empty;
        public string NombreCliente { get; private set; } = string.Empty;

        private Devolucione() { }

        public static Devolucione Crear(int idPedido, int idUsuario, int? idCliente, string motivo)
        {
            if (idPedido <= 0)
                throw new DomainException("El pedido es requerido.");
            if (idUsuario <= 0)
                throw new DomainException("El usuario es requerido.");
            if (string.IsNullOrWhiteSpace(motivo))
                throw new DomainException("El motivo es requerido.");

            var d = new Devolucione { Fecha = DateTime.Now, Estado = "solicitada" };
            d.Actualizar(idPedido, idUsuario, idCliente, motivo);
            return d;
        }

        public void Actualizar(int idPedido, int idUsuario, int? idCliente, string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new DomainException("El motivo es requerido.");

            Idpedido = idPedido;
            Idusuario = idUsuario;
            Idcliente = idCliente;
            Motivo = motivo.Trim();
        }

        public void CambiarEstado(string nuevoEstado)
        {
            var estadosValidos = new[] { "solicitada", "aprobada", "rechazada" };
            if (!estadosValidos.Contains(nuevoEstado))
                throw new DomainException("Estado de devolución no válido.");
            Estado = nuevoEstado;
        }
    }
}