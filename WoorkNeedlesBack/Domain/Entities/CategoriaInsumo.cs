using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class CategoriaInsumo
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public DateTime? Fechacreacion { get; private set; }

        private CategoriaInsumo() { }

        public static CategoriaInsumo Crear(string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre es requerido.");
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new DomainException("La descripción es requerida.");

            var c = new CategoriaInsumo { Fechacreacion = DateTime.Now };
            c.Actualizar(nombre, descripcion);
            return c;
        }

        public void Actualizar(string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre es requerido.");
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new DomainException("La descripción es requerida.");

            Nombre = nombre.Trim();
            Descripcion = descripcion.Trim();
        }
    }
}