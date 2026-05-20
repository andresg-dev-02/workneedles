using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class ProductoTalla
    {
        public int Id { get; private set; }
        public int? Idproducto { get; private set; }
        public int? Idtalla { get; private set; }
        public DateTime Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }
        public string NombreProducto { get; private set; } = string.Empty;
        public string NombreTalla { get; private set; } = string.Empty;

        private ProductoTalla() { }

        public static ProductoTalla Crear(int idProducto, int idTalla)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idTalla <= 0)
                throw new DomainException("La talla es requerida.");

            var pt = new ProductoTalla { Fechacreacion = DateTime.Now };
            pt.Actualizar(idProducto, idTalla);
            return pt;
        }

        public void Actualizar(int idProducto, int idTalla)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idTalla <= 0)
                throw new DomainException("La talla es requerida.");

            Idproducto = idProducto;
            Idtalla = idTalla;
            Fechamodificacion = DateTime.Now;
        }
    }
}