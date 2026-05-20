
namespace Application.DTOs.Productos
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigohex { get; set; } = string.Empty;
    }

    public class CreateColorDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Codigohex { get; set; } = string.Empty;
    }

    public class UpdateColorDto : CreateColorDto { }


    
    public class ProductoColorDto
    {
        public int Id { get; set; }
        public string Nombrecolor { get; set; } = string.Empty;
        public string Codigohex { get; set; } = string.Empty;
    }

    public class CreateProductoColorDto
    {
        public int Idproducto { get; set; }
        public int Idcolor { get; set; }
    }

    public class UpdateProductoColorDto : CreateProductoColorDto { }

}
