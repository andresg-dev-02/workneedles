using Application.DTOs.Pago;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Pago
{
    public class CreatePago(IUnitOfWork unitofWork)
    {
        public async Task Execute(CreatePagoDto dto)
        {
            var pago = Domain.Entities.Pago.Crear(dto.Idpedido, dto.Idusuario,
                dto.Monto, dto.Tipopago, dto.Referencia, dto.Observaciones);
            await unitofWork.Pagos.AddAsync(pago);
            await unitofWork.SaveAsync();
        }
    }

    public class GetAllPagos(IUnitOfWork unitofWork, IMapper mapper)
    {
        public async Task<IEnumerable<PagoDto>> Execute(int idPedido)
        {
            var options = new QueryOptions<Domain.Entities.Pago>()
                .AddInclude("IdusuarioNavigation");
            var pagos = await unitofWork.Pagos.GetAllAsync(options);
            return mapper.Map<IEnumerable<PagoDto>>(
                pagos.Where(p => p.Idpedido == idPedido));
        }
    }

    public class CambiarEstadoPago(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id, CambiarEstadoPagoDto dto)
        {
            var pago = await unitofWork.Pagos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pago no encontrado.");
            pago.CambiarEstado(dto.Estado);
            unitofWork.Pagos.Update(pago);
            await unitofWork.SaveAsync();
        }
    }

    public class DeletePago(IUnitOfWork unitofWork)
    {
        public async Task Execute(int id)
        {
            var pago = await unitofWork.Pagos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pago no encontrado.");
            unitofWork.Pagos.Delete(pago);
            await unitofWork.SaveAsync();
        }
    }
}