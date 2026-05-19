using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Products.Colores
{
    public class CreateProductoColor(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateProductoColorDto productoColorCreatedto)
        {
            var productoColore = ProductoColore.Crear(productoColorCreatedto.Idproducto, productoColorCreatedto.Idcolor);
            await unitofwork.ProductoColores.AddAsync(productoColore);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllProductoColor(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoColorDto>> Execute(int id)
        {
            var options = new QueryOptions<ProductoColore>()
                .AddInclude("IdproductoNavigation")
                .AddInclude("IdcolorNavigation");

            var productoColores = await unitofwork.ProductoColores.GetAllAsync(options);
            return mapper.Map<IEnumerable<ProductoColorDto>>(
                productoColores.Where(pc => pc.Idproducto == id));
        }
    }

    public class DeleteProductoColor(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var productoColore = await unitofwork.ProductoColores.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Relación producto-color no encontrada.");
            unitofwork.ProductoColores.Delete(productoColore);
            await unitofwork.SaveAsync();
        }
    }
}
