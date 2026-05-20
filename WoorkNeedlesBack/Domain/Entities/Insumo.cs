using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Insumo
    {
        public int Id { get; private set; }
        public int Idcategoria { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public string Unidadmedida { get; private set; } = string.Empty;
        public decimal Stockactual { get; private set; }
        public decimal Stockalerta { get; private set; }
        public decimal Precio { get; private set; }
        public string? Proveedor { get; private set; }
        public bool Activo { get; private set; }
        public DateTime? Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }
        public string Categoria { get; private set; } = string.Empty;

        private Insumo() { }

        public static Insumo Crear(int idCategoria, string nombre, string descripcion,
        string unidadMedida, decimal stockActual, decimal stockAlerta,
        decimal precio, string? proveedor)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre es requerido.");
            if (precio <= 0)
                throw new DomainException("El precio debe ser mayor a 0.");
            if (stockActual < 0)
                throw new DomainException("El stock no puede ser negativo.");

            var i = new Insumo { Activo = true, Fechacreacion = DateTime.Now };
            i.Actualizar(idCategoria, nombre, descripcion, unidadMedida,
                stockActual, stockAlerta, precio, proveedor);
            return i;
        }

        public void Actualizar(int idCategoria, string nombre, string descripcion,
            string unidadMedida, decimal stockActual, decimal stockAlerta,
            decimal precio, string? proveedor)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre es requerido.");
            if (precio <= 0)
                throw new DomainException("El precio debe ser mayor a 0.");
            if (stockActual < 0)
                throw new DomainException("El stock no puede ser negativo.");

                 Idcategoria = idCategoria;
            Nombre = nombre.Trim();
            Descripcion = descripcion.Trim();
            Unidadmedida = unidadMedida.Trim();
            Stockactual = stockActual;
            Stockalerta = stockAlerta;
            Precio = precio;
            Proveedor = proveedor?.Trim();
            Fechamodificacion = DateTime.Now;
        }
    }
}