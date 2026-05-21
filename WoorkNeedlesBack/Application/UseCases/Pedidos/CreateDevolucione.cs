using Application.DTOs.Pedido;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Pedidos
{
    public class CreateDevolucione(IUnitOfWork unitofWork)
    {
        public async Task Execute(CreateDevolucioneDto dto)
        {
            var devolucion = Domain.Entities.Devolucione.Crear(dto.Idpedido, dto.Idusuario,
                dto.Idcliente, dto.Motivo);
            await unitofWork.Devoluciones.AddAsync(devolucion);
            await unitofWork.SaveAsync();
        }
    }

    public class GetAllDevoluciones(IUnitOfWork unitofWork, IMapper mapper)
    {
        public async Task<IEnumerable<DevolucionesDto>> Execute(int idPedido)
        {
            var options = new QueryOptions<Devolucione>()
                .AddInclude("IdusuarioNavigation")
                .AddInclude("IdclienteNavigation");
            var devoluciones = await unitofWork.Devoluciones.GetAllAsync(options);
            return mapper.Map<IEnumerable<DevolucionesDto>>(
                devoluciones.Where(d => d.Idpedido == idPedido));
        }
    }

    public class CambiarEstadoDevolucione(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id, CambiarEstadoDevolucioneDto dto)
        {
            var devolucion = await unitofWork.Devoluciones.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Devolución no encontrada.");
            devolucion.CambiarEstado(dto.Estado);
            unitofWork.Devoluciones.Update(devolucion);
            await unitofWork.SaveAsync();
        }
    }

    public class DeleteDevolucione(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id)
        {
            var devolucion = await unitofWork.Devoluciones.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Devolución no encontrada.");
            unitofWork.Devoluciones.Delete(devolucion);
            await unitofWork.SaveAsync();
        }
    }
}