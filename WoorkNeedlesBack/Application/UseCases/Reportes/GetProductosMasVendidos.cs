using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Reportes;
using Domain.Entities;
using Domain.Ports.Output.UnitOfWork;
using Domain.Specification;

namespace Application.UseCases.Reportes
{
    public class GetProductosMasVendidos(IUnitOfWork unitofwork)
    {
        public async Task<IEnumerable<ProductoMasVendidoDto>> Execute()
        {
            var estadosValidos = new[] { "enviado", "entregado" };

            var pedidos = await unitofwork.Pedidos.GetAllAsync();
            var idsPedidosValidos = pedidos
                .Where(p => estadosValidos.Contains(p.Estado))
                .Select(p => p.Id)
                .ToHashSet();

            var options = new QueryOptions<DetallePedido>()
                .AddInclude("IdproductoNavigation");
            var detalles = await unitofwork.DetallesPedido.GetAllAsync(options);

            return detalles
                .Where(d => idsPedidosValidos.Contains(d.Idpedido))
                .GroupBy(d => d.NombreProducto)
                .Select(g => new ProductoMasVendidoDto
                {
                    NombreProducto = g.Key,
                    TotalVendido = g.Sum(d => d.Cantidad),
                    TotalIngresos = g.Sum(d => d.Subtotal)
                })
                .OrderByDescending(p => p.TotalVendido)
                .ToList();
        }
    }

    public class GetIngresosMensuales(IUnitOfWork unitofwork)
    {
        public async Task<IEnumerable<IngresoMensualDto>> Execute()
        {
            var estadosValidos = new[] { "enviado", "entregado" };

            var detalleOptions = new QueryOptions<DetallePedido>()
                .AddInclude("IdproductoNavigation");

            var pedidos = await unitofwork.Pedidos.GetAllAsync();
            var detalles = await unitofwork.DetallesPedido.GetAllAsync(detalleOptions);

            return pedidos
                .Where(p => p.Fechapedido.HasValue && estadosValidos.Contains(p.Estado))
                .GroupBy(p => new { p.Fechapedido!.Value.Month, p.Fechapedido.Value.Year })
                .Select(g =>
                {
                    var idsPedidos = g.Select(p => p.Id).ToList();
                    var detallesGrupo = detalles.Where(d => idsPedidos.Contains(d.Idpedido));

                    return new IngresoMensualDto
                    {
                        Mes = new DateTime(g.Key.Year, g.Key.Month, 1)
                            .ToString("MMMM", new System.Globalization.CultureInfo("es-CO")),
                        Anio = g.Key.Year,
                        TotalIngresos = detallesGrupo.Sum(d => d.Subtotal),
                        TotalPedidos = g.Count()
                    };
                })
                .OrderByDescending(i => i.Anio)
                .ThenByDescending(i => i.Mes)
                .ToList();
        }
    }

    public class GetFrecuenciaPedidos(IUnitOfWork unitofwork)
    {
        public async Task<IEnumerable<FrecuenciaPedidoDto>> Execute()
        {
            var estadosValidos = new[] { "enviado", "entregado" };

            var pedidoOptions = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation");
            var detalleOptions = new QueryOptions<DetallePedido>()
                .AddInclude("IdproductoNavigation");

            var pedidos = await unitofwork.Pedidos.GetAllAsync(pedidoOptions);
            var detalles = await unitofwork.DetallesPedido.GetAllAsync(detalleOptions);

            return pedidos
                .Where(p => estadosValidos.Contains(p.Estado))
                .GroupBy(p => p.NombreCliente)
                .Select(g =>
                {
                    var detallesCliente = detalles
                        .Where(d => g.Select(p => p.Id).Contains(d.Idpedido));

                    return new FrecuenciaPedidoDto
                    {
                        NombreCliente = g.Key,
                        TotalPedidos = g.Count(),
                        TotalGastado = detallesCliente.Sum(d => d.Subtotal),
                    };
                })
                .OrderByDescending(f => f.TotalPedidos)
                .ToList();
        }
    }

    public class GetComportamientoClientes(IUnitOfWork unitofwork)
    {
        public async Task<IEnumerable<ComportamientoClienteDto>> Execute()
        {
            var estadosValidos = new[] { "enviado", "entregado" };

            var pedidoOptions = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation");
            var detalleOptions = new QueryOptions<DetallePedido>()
                .AddInclude("IdproductoNavigation");

            var pedidos = await unitofwork.Pedidos.GetAllAsync(pedidoOptions);
            var detalles = await unitofwork.DetallesPedido.GetAllAsync(detalleOptions);

            return pedidos
                .Where(p => estadosValidos.Contains(p.Estado))
                .GroupBy(p => new { p.NombreCliente, p.Idcliente })
                .Select(g =>
                {
                    var detallesCliente = detalles
                        .Where(d => g.Select(p => p.Id).Contains(d.Idpedido));

                    var productoFavorito = detallesCliente
                        .GroupBy(d => d.NombreProducto)
                        .OrderByDescending(d => d.Sum(x => x.Cantidad))
                        .FirstOrDefault()?.Key ?? string.Empty;

                    return new ComportamientoClienteDto
                    {
                        NombreCliente = g.Key.NombreCliente,
                        TotalPedidos = g.Count(),
                        TotalGastado = detallesCliente.Sum(d => d.Subtotal),
                        UltimoPedido = g.Max(p => p.Fechapedido),
                        ProductoFavorito = productoFavorito
                    };
                })
                .OrderByDescending(c => c.TotalGastado)
                .ToList();
        }
    }
}