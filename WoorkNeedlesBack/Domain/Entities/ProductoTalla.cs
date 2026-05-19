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
        public int Stock { get; private set; }
        public DateTime Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }

        private ProductoTalla() { }

        public static ProductoTalla Crear(int idProducto, int idTalla, int stock)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idTalla <= 0)
                throw new DomainException("La talla es requerida.");
            if (stock < 0)
                throw new DomainException("El stock no puede ser negativo.");

            var pt = new ProductoTalla { Fechacreacion = DateTime.Now };
            pt.Actualizar(idProducto, idTalla, stock);
            return pt;
        }

        public void Actualizar(int idProducto, int idTalla, int stock)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idTalla <= 0)
                throw new DomainException("La talla es requerida.");
            if (stock < 0)
                throw new DomainException("El stock no puede ser negativo.");

            Idproducto = idProducto;
            Idtalla = idTalla;
            Stock = stock;
            Fechamodificacion = DateTime.Now;
        }
    }
}