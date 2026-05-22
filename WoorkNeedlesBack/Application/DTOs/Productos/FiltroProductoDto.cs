using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos
{
    public class FiltroProductoDto
    {
        public string? Nombre { get; set; }
        public string? Categoria { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Genero { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public bool? Disponible { get; set; }
    }
}