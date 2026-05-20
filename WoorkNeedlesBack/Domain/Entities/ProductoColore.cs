using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class ProductoColore
    {
        public int Id { get; private set; }
        public int? Idproducto { get; private set; }
        public int? Idcolor { get; private set; }
        public DateTime Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }
        public string Nombrecolor { get; private set; } = string.Empty;
        public string Codigohex { get; private set; } = string.Empty;

        private ProductoColore() { }

        public static ProductoColore Crear(int idProducto, int idColor)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idColor <= 0)
                throw new DomainException("El color es requerido.");

            var pc = new ProductoColore { Fechacreacion = DateTime.Now };
            pc.Actualizar(idProducto, idColor);
            return pc;
        }

        public void Actualizar(int idProducto, int idColor)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idColor <= 0)
                throw new DomainException("El color es requerido.");

            Idproducto = idProducto;
            Idcolor = idColor;
            Fechamodificacion = DateTime.Now;
        }
    }
}