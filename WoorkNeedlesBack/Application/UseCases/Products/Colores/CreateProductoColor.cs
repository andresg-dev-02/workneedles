using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Colores
{
    public class CreateProductoColore(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreateProductoColorDto productoColorCreatedto)
        {
            var productoColore = ProductoColore.Crear(productoColorCreatedto.Idproducto, productoColorCreatedto.Idcolor);
            await unitofwork.ProductoColores.AddAsync(productoColore);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllProductoColores(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoColorDto>> Execute()
        {
            var productoColores = await unitofwork.ProductoColores.GetAllAsync();
            return mapper.Map<IEnumerable<ProductoColorDto>>(productoColores);
        }
    }

    public class DeleteProductoColore(IUnitOfWork unitofwork)
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
