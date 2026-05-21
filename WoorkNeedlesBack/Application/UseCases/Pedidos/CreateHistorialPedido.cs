using Application.DTOs.Pedido;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Pedidos
{
    public class CreateHistorialPedido(IUnitOfWork unitofWork)
    {
        public async Task Execute(CreateHistorialPedidoDto dto)
        {
            var historial = Domain.Entities.HistorialPedido.Crear(dto.Idpedido, dto.Idusuario,
                dto.Estadoanterior, dto.Estadoactual, dto.Observacion);
            await unitofWork.HistorialPedidos.AddAsync(historial);
            await unitofWork.SaveAsync();
        }
    }

    public class GetAllHistorialPedido(IUnitOfWork unitofWork, IMapper mapper)
    {
        public async Task<IEnumerable<HistorialPedidoDto>> Execute(int idPedido)
        {
            var options = new QueryOptions<HistorialPedido>()
                .AddInclude("IdusuarioNavigation");
            var historial = await unitofWork.HistorialPedidos.GetAllAsync(options);
            return mapper.Map<IEnumerable<HistorialPedidoDto>>(
                historial.Where(h => h.Idpedido == idPedido));
        }
    }

    public class DeleteHistorialPedido(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id)
        {
            var historial = await unitofWork.HistorialPedidos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Historial no encontrado.");
            unitofWork.HistorialPedidos.Delete(historial);
            await unitofWork.SaveAsync();
        }
    }
}