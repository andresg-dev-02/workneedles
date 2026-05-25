using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos
{
    public class InventarioDto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string NombreColor { get; set; } = string.Empty;
        public string NombreTalla { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }

    public class CreateInventarioDto
    {
        public int Idproducto { get; set; }
        public int Idcolor { get; set; }
        public int Idtalla { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateInventarioDto
    {
        public int Stock { get; set; }
    }
}