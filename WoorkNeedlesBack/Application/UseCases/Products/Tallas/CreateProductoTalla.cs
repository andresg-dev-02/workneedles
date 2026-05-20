using Application.DTOs.Productos;
using Domain.Entities;
using AutoMapper;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Products.Tallas
{
    public class CreateProductoTalla(IUnitOfWork unitofwork)    
    {
        public async Task Execute(CreateProductoTallaDto crearProductodto)
        {
            var productoTalla = ProductoTalla.Crear(crearProductodto.Idproducto, crearProductodto.Idtalla);
            await unitofwork.ProductoTallas.AddAsync(productoTalla);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllProductoTallas(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<ProductoTallaDto>> Execute(int id)
        {
            var options = new QueryOptions<ProductoTalla>()
                .AddInclude("IdproductoNavigation")
                .AddInclude("IdtallaNavigation");

            var productoTallas = await unitofwork.ProductoTallas.GetAllAsync(options);
            return mapper.Map<IEnumerable<ProductoTallaDto>>(
                productoTallas.Where(pt => pt.Idproducto == id));
        }
    }

    public class UpdateProductoTalla(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdateProductoTallaDto actualizarProductodto)
        {
            var productoTalla = await unitofwork.ProductoTallas.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Relación producto-talla no encontrada.");
            productoTalla.Actualizar(actualizarProductodto.Idproducto, actualizarProductodto.Idtalla);
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