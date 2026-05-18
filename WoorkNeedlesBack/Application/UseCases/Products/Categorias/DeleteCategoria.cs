using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Application.Interfaces.Product;

namespace Application.UseCases.Products.Categorias
{
   
    public class DeleteCategoria(ICategoriaProductoRepository repository)
    {
        public async Task EliminarCategoriaProducto(int id) => await repository.DeleteAsync(id);
    }
}
