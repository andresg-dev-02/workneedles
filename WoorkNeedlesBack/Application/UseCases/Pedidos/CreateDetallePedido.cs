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
            if (dto.Idinventario.HasValue)
            {
                var inventario = await unitofWork.Inventario.GetByIdAsync(dto.Idinventario.Value)
                    ?? throw new KeyNotFoundException("Inventario no encontrado.");
                inventario.RestarStock(dto.Cantidad);
                unitofWork.Inventario.Update(inventario);
            }

            var detalle = Domain.Entities.DetallePedido.Crear(dto.Idpedido, dto.Idproducto,
                dto.Idinventario, dto.Cantidad, dto.Preciounitario);
            await unitofWork.DetallesPedido.AddAsync(detalle);

            var pedido = await unitofWork.Pedidos.GetByIdAsync(dto.Idpedido)
            ?? throw new KeyNotFoundException("Pedido no encontrado.");

            var detalles = await unitofWork.DetallesPedido.GetAllAsync();
            var subtotalReal = detalles
                .Where(d => d.Idpedido == dto.Idpedido)
                .Sum(d => d.Subtotal) + detalle.Subtotal;

            pedido.RecalcularTotales(subtotalReal);
            unitofWork.Pedidos.Update(pedido);
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

            // 1. Restaurar stock anterior y restar el nuevo
            if (detalle.Idinventario.HasValue)
            {
                var inventario = await unitofWork.Inventario.GetByIdAsync(detalle.Idinventario.Value);
                if (inventario != null)
                {
                    inventario.RestaurarStock(detalle.Cantidad);      // devuelve lo anterior
                    inventario.RestarStock(dto.Cantidad);              // resta lo nuevo
                    unitofWork.Inventario.Update(inventario);
                }
            }

            // 2. Actualizar detalle
            detalle.Actualizar(detalle.Idpedido, detalle.Idproducto,
                dto.Idinventario, dto.Cantidad, dto.Preciounitario);
            unitofWork.DetallesPedido.Update(detalle);

            // 3. Recalcular total del pedido
            var pedido = await unitofWork.Pedidos.GetByIdAsync(detalle.Idpedido)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");

            var todosDetalles = await unitofWork.DetallesPedido.GetAllAsync();
            var subtotalReal = todosDetalles
                .Where(d => d.Idpedido == detalle.Idpedido && d.Id != id)
                .Sum(d => d.Subtotal) + (dto.Cantidad * dto.Preciounitario);

            pedido.RecalcularTotales(subtotalReal);
            unitofWork.Pedidos.Update(pedido);
            await unitofWork.SaveAsync();
        }
    }

    public class DeleteDetallePedido(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id)
        {
            var detalle = await unitofWork.DetallesPedido.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Detalle de pedido no encontrado.");

            // 1. Restaurar stock
            if (detalle.Idinventario.HasValue)
            {
                var inventario = await unitofWork.Inventario.GetByIdAsync(detalle.Idinventario.Value);
                if (inventario != null)
                {
                    inventario.RestaurarStock(detalle.Cantidad);
                    unitofWork.Inventario.Update(inventario);
                }
            }

            // 2. Recalcular total del pedido
            var pedido = await unitofWork.Pedidos.GetByIdAsync(detalle.Idpedido)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");

            var todosDetalles = await unitofWork.DetallesPedido.GetAllAsync();
            var subtotalReal = todosDetalles
                .Where(d => d.Idpedido == detalle.Idpedido && d.Id != id)
                .Sum(d => d.Subtotal);

            pedido.RecalcularTotales(subtotalReal);
            unitofWork.Pedidos.Update(pedido);

            // 3. Eliminar detalle
            unitofWork.DetallesPedido.Delete(detalle);
            await unitofWork.SaveAsync();
        }
    }
}