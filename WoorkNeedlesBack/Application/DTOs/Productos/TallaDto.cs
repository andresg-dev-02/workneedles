using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos
{
    public class TallaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class CreateTallaDto
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class UpdateTallaDto : CreateTallaDto { }


    
    public class ProductoTallaDto
    {
        public int Id { get; set; }
        public int? Idproducto { get; set; }
        public int? Idtalla { get; set; }
        public int Stock { get; set; }
    }

    public class CreateProductoTallaDto
    {
        public int Idproducto { get; set; }
        public int Idtalla { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateProductoTallaDto : CreateProductoTallaDto { }

}