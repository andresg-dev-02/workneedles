using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Colore
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Codigohex { get; private set; } = string.Empty;
        public DateTime Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }

        private Colore() { }

        public static Colore Crear(string nombre, string codigohex)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre del color es requerido.");
            if (string.IsNullOrWhiteSpace(codigohex))
                throw new DomainException("El código hex es requerido.");

            var c = new Colore { Fechacreacion = DateTime.Now };
            c.Actualizar(nombre, codigohex);
            return c;
        }

        public void Actualizar(string nombre, string codigohex)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre del color es requerido.");
            if (string.IsNullOrWhiteSpace(codigohex))
                throw new DomainException("El código hex es requerido.");

            Nombre = nombre.Trim();
            Codigohex = codigohex.Trim();
            Fechamodificacion = DateTime.Now;
        }
    }
}