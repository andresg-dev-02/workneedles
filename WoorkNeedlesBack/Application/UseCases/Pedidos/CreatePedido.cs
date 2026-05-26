using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Pedido;
using Domain.Specification;
using Domain.Ports.Output.UnitOfWork;
using Domain.Ports.Output.Email;

namespace Application.UseCases.Pedidos
{
    public class CreatePedido(IUnitOfWork unitofwork)
    {
        public async Task Execute(CreatePedidoDto dto)
        {
            var pedido = Domain.Entities.Pedido.Crear(
                dto.IdCliente, dto.IdUsuario,
                dto.FechEntregaAprox, dto.FechaEntrega,
                dto.DireccionEntrega, dto.Observaciones,
                dto.Descuento);
            await unitofwork.Pedidos.AddAsync(pedido);
            await unitofwork.SaveAsync();
        }
    }

    public class GetAllPedidos(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<PedidoDto>> Execute()
        {
            var options = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation")
                .AddInclude("IdusuarioNavigation");
            var pedidos = await unitofwork.Pedidos.GetAllAsync(options);
            return mapper.Map<IEnumerable<PedidoDto>>(pedidos);
        }
    }

    public class GetPedidoById(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<PedidoDto> Execute(int id)
        {
            var options = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation")
                .AddInclude("IdusuarioNavigation");
            var pedido = await unitofwork.Pedidos.GetByIdAsync(id, options)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");
            return mapper.Map<PedidoDto>(pedido);
        }
    }

    public class GetPedidosByCliente(IUnitOfWork unitofwork, IMapper mapper)
    {
        public async Task<IEnumerable<PedidoDto>> Execute(int idCliente)
        {
            var options = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation")
                .AddInclude("IdusuarioNavigation");
            var pedidos = await unitofwork.Pedidos.GetAllAsync(options);
            return mapper.Map<IEnumerable<PedidoDto>>(
                pedidos.Where(p => p.Idcliente == idCliente));
        }
    }

    public class UpdatePedido(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id, UpdatePedidoDto dto)
        {
            var pedido = await unitofwork.Pedidos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");
            pedido.Actualizar(
                pedido.Idcliente, pedido.Idusuario,
                dto.FechEntregaAprox, dto.FechaEntrega,
                dto.DireccionEntrega, dto.Observaciones,
                dto.Descuento); 
            unitofwork.Pedidos.Update(pedido);
            await unitofwork.SaveAsync();
        }
    }

    public class CambiarEstadoPedido(IUnitOfWork unitofwork, IEmailService emailService)
    {
        public async Task Execute(int id, CambiarEstadoPedidoDto dto)
        {
            var pedido = await unitofwork.Pedidos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");

            if (dto.Estado.ToLower() == "cancelado")
            {
                var detalles = await unitofwork.DetallesPedido.GetAllAsync();
                var detallesPedido = detalles.Where(d => d.Idpedido == id).ToList();

                foreach (var detalle in detallesPedido)
                {
                    if (detalle.Idinventario.HasValue)
                    {
                        var inventario = await unitofwork.Inventario.GetByIdAsync(detalle.Idinventario.Value);
                        if (inventario != null)
                        {
                            inventario.RestaurarStock(detalle.Cantidad);
                            unitofwork.Inventario.Update(inventario);
                        }
                    }
                }
            }

            if (dto.Estado.ToLower() == "enviado")
            {
                var cliente = await unitofwork.Clientes.GetByIdAsync(pedido.Idcliente)
                    ?? throw new KeyNotFoundException("Cliente no encontrado.");

                var token = Guid.NewGuid().ToString("N");
                pedido.AsignarToken(token);

                await emailService.EnviarCorreoEnvioAsync(
                    cliente.Email,
                    cliente.Nombres,
                    cliente.Apellidos,
                    pedido.Id,
                    pedido.Direccionentrega,
                    pedido.Fechentregaaprox,
                    token);
            }

            pedido.CambiarEstado(dto.Estado);
            unitofwork.Pedidos.Update(pedido);
            await unitofwork.SaveAsync();
        }
    }

    public class DeletePedido(IUnitOfWork unitofwork)
    {
        public async Task Execute(int id)
        {
            var pedido = await unitofwork.Pedidos.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido no encontrado.");
            unitofwork.Pedidos.Delete(pedido);
            await unitofwork.SaveAsync();
        }
    }
}