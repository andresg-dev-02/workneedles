using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos
{
    public class InsumoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Unidadmedida { get; set; } = string.Empty;
        public decimal Stockactual { get; set; }
        public decimal Stockalerta { get; set; }
        public decimal Precio { get; set; }
        public string? Proveedor { get; set; }
        public bool Activo { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime? Fechacreacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }

    public class CreateInsumoDto
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Unidadmedida { get; set; } = string.Empty;
        public decimal Stockactual { get; set; }
        public decimal Stockalerta { get; set; }
        public decimal Precio { get; set; }
        public string? Proveedor { get; set; }
    }

    public class UpdateInsumoDto : CreateInsumoDto { }




    public class CategoriaInsumoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class CreateCategoriaInsumoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class UpdateCategoriaInsumoDto : CreateCategoriaInsumoDto { }






    public class InsumosProductoDto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string NombreInsumo { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
    }

    public class CreateInsumosProductoDto
    {
        public int Idproducto { get; set; }
        public int Idinsumo { get; set; }
        public decimal Cantidad { get; set; }
    }

    public class UpdateInsumosProductoDto
    {
        public decimal Cantidad { get; set; }
    }
}