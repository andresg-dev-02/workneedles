using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class InsumosProducto
    {
        public int Id { get; private set; }
        public int Idproducto { get; private set; }
        public int Idinsumo { get; private set; }
        public decimal Cantidad { get; private set; }
        public string NombreProducto { get; private set; } = string.Empty;
        public string NombreInsumo { get; private set; } = string.Empty;
        public string UnidadMedida { get; private set; } = string.Empty;

        private InsumosProducto() { }

        public static InsumosProducto Crear(int idProducto, int idInsumo, decimal cantidad)
        {
            if (idProducto <= 0)
                throw new DomainException("El producto es requerido.");
            if (idInsumo <= 0)
                throw new DomainException("El insumo es requerido.");
            if (cantidad <= 0)
                throw new DomainException("La cantidad debe ser mayor a 0.");

            var ip = new InsumosProducto();
            ip.Actualizar(idProducto, idInsumo, cantidad);
            return ip;
        }

        public void Actualizar(int idProducto, int idInsumo, decimal cantidad)
        {
            if (cantidad <= 0)
                throw new DomainException("La cantidad debe ser mayor a 0.");

            Idproducto = idProducto;
            Idinsumo = idInsumo;
            Cantidad = cantidad;
        }
    }
}