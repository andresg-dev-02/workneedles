using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos
{
    
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



    
    public class ColoreDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigohex { get; set; } = string.Empty;
    }

    public class CreateColoreDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Codigohex { get; set; } = string.Empty;
    }

    public class UpdateColoreDto : CreateColoreDto { }




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




    public class ProductoColoreDto
    {
        public int Id { get; set; }
        public int? Idproducto { get; set; }
        public int? Idcolor { get; set; }
    }

    public class CreateProductoColoreDto
    {
        public int Idproducto { get; set; }
        public int Idcolor { get; set; }
    }

}