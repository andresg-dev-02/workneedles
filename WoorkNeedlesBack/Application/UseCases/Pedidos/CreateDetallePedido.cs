using Application.DTOs.Pedido;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Pedidos
{
    public class CreateDetallePedido(IUnitOfWork unitofWork)
    {
        public async Task Execute(CreateDetallePedidoDto dto)
        {
            var detalle = Domain.Entities.DetallePedido.Crear(dto.Idpedido, dto.Idproducto,
                dto.Idinventario, dto.Cantidad, dto.Preciounitario);
            await unitofWork.DetallesPedido.AddAsync(detalle);
            await unitofWork.SaveAsync();
        }
    }

    public class GetAllDetallesPedido(IUnitOfWork unitofWork, IMapper mapper)
    {
        public async Task<IEnumerable<DetallePedidoDto>> Execute(int idPedido)
        {
            var options = new QueryOptions<Domain.Entities.DetallePedido>()
                .AddInclude("IdproductoNavigation")
                .AddInclude("IdinventarioNavigation.IdtallaNavigation")
                .AddInclude("IdinventarioNavigation.IdcolorNavigation");
            var detalles = await unitofWork.DetallesPedido.GetAllAsync(options);
            return mapper.Map<IEnumerable<DetallePedidoDto>>(
                detalles.Where(d => d.Idpedido == idPedido));
        }
    }

    public class UpdateDetallePedido(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id, UpdateDetallePedidoDto dto)
        {
            var detalle = await unitofWork.DetallesPedido.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Detalle de pedido no encontrado.");
            detalle.Actualizar(detalle.Idpedido, detalle.Idproducto,
                dto.Idinventario, dto.Cantidad, dto.Preciounitario);
            unitofWork.DetallesPedido.Update(detalle);
            await unitofWork.SaveAsync();
        }
    }

    public class DeleteDetallePedido(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id)
        {
            var detalle = await unitofWork.DetallesPedido.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Detalle de pedido no encontrado.");
            unitofWork.DetallesPedido.Delete(detalle);
            await unitofWork.SaveAsync();
        }
    }
}