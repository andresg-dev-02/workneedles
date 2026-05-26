using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output.UnitOfWork;
using Domain.Ports.Output.Email;
using Domain.Specification;
using Domain.Exceptions;

namespace Application.UseCases.Pedidos
{
    public class ConfirmarEntrega(IUnitOfWork unitofwork, IEmailService emailService)
    {
        public async Task Execute(int id, string token)
        {
            var pedido = await unitofwork.Pedidos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");

            if (pedido.TokenConfirmacion != token)
                throw new DomainException("Token inválido.");

            var cliente = await unitofwork.Clientes.GetByIdAsync(pedido.Idcliente)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");

            pedido.CambiarEstado("entregado");
            pedido.LimpiarToken();
            unitofwork.Pedidos.Update(pedido);
            await unitofwork.SaveAsync();

            await emailService.EnviarCorreoConfirmacionAsync(
                cliente.Email, cliente.Nombres, cliente.Apellidos, pedido.Id);
        }
    }
}