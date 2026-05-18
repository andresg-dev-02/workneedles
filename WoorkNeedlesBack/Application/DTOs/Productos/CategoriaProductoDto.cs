using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos
{
    public class CategoriaProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime? Fechacreacion { get; set; }
    }

    public class CreateCategoriaProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class UpdateCategoriaProductoDto : CreateCategoriaProductoDto { }
}