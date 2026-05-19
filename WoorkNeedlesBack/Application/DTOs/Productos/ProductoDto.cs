using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Productos;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string? Material { get; set; }
    public decimal Preciobase { get; set; }
    public string? Urlimagen { get; set; }
    public bool Activo { get; set; }
    public DateTime? Fechacreacion { get; set; }
    public string Categoria { get; set; } = string.Empty;
}

public class CreateProductoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string? Material { get; set; }
    public decimal Preciobase { get; set; }
    public string? Urlimagen { get; set; }
    public int IdCategoria { get; set; }
}

public class UpdateProductoDto : CreateProductoDto { }