using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Talla
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public DateTime Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }

        private Talla() { }

        public static Talla Crear(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre de la talla es requerido.");

            var t = new Talla { Fechacreacion = DateTime.Now };
            t.Actualizar(nombre);
            return t;
        }

        public void Actualizar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre de la talla es requerido.");

            Nombre = nombre.Trim();
            Fechamodificacion = DateTime.Now;
        }
    }
}