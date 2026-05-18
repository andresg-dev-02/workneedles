using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Productos;
using Application.Interfaces.Product;

namespace Application.UseCases.Products.Producto
{
    public class DeleteProducto(IProductoRepository repository)
    {
        public async Task EliminarProducto(int id) => await repository.DeleteAsync(id);
    }
}