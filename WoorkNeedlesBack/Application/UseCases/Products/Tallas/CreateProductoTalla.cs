using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;

namespace Application.UseCases.Products.Tallas
{
    public class CreateProductoTalla(IUnitOfWork unitofwork)    
    {
        public async Task Execute(CreateProductoTallaDto crearProductodto)
        {
            var productoTalla = ProductoTalla.Crear(crearProductodto.Idproducto, crearProductodto.Idtalla, crearProductodto.Stock);
            await unitofwork.ProductoTallas.AddAsync(productoTalla);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllProductoTallas(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoTallaDto>> Execute()
        {
            var productoTallas = await unitofwork.ProductoTallas.GetAllAsync();
            return mapper.Map<IEnumerable<ProductoTallaDto>>(productoTallas);
        }
    }

    public class UpdateProductoTalla(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateProductoTallaDto actualizarProductodto)
        {
            var productoTalla = await unitofwork.ProductoTallas.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Relación producto-talla no encontrada.");
            productoTalla.Actualizar(actualizarProductodto.Idproducto, actualizarProductodto.Idtalla, actualizarProductodto.Stock);
            unitofwork.ProductoTallas.Update(productoTalla);
            await unitofwork.SaveAsync();
        }
    }

    public class DeleteProductoTalla(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var productoTalla = await unitofwork.ProductoTallas.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Relación producto-talla no encontrada.");
            unitofwork.ProductoTallas.Delete(productoTalla);
            await unitofwork.SaveAsync();
        }
    }
}