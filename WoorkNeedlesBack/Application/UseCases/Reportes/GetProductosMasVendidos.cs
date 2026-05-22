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
            var options = new QueryOptions<Domain.Entities.DetallePedido>()
                .AddInclude("IdproductoNavigation");

            var detalles = await unitofwork.DetallesPedido.GetAllAsync(options);

            return detalles
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
            var pedidos = await unitofwork.Pedidos.GetAllAsync();

            return pedidos
                .Where(p => p.Fechapedido.HasValue)
                .GroupBy(p => new { p.Fechapedido!.Value.Month, p.Fechapedido.Value.Year })
                .Select(g => new IngresoMensualDto
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1)
                        .ToString("MMMM", new System.Globalization.CultureInfo("es-CO")),
                    Anio = g.Key.Year,
                    TotalIngresos = g.Sum(p => p.Total),
                    TotalPedidos = g.Count()
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
            var options = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation");

            var pedidos = await unitofwork.Pedidos.GetAllAsync(options);

            return pedidos
                .GroupBy(p => p.NombreCliente)
                .Select(g => new FrecuenciaPedidoDto
                {
                    NombreCliente = g.Key,
                    TotalPedidos = g.Count(),
                    TotalGastado = g.Sum(p => p.Total)
                })
                .OrderByDescending(f => f.TotalPedidos)
                .ToList();
        }
    }

    public class GetComportamientoClientes(IUnitOfWork unitofwork)
    {
        public async Task<IEnumerable<ComportamientoClienteDto>> Execute()
        {
            var pedidoOptions = new QueryOptions<Pedido>()
                .AddInclude("IdclienteNavigation");

            var detalleOptions = new QueryOptions<DetallePedido>()
                .AddInclude("IdproductoNavigation");

            var pedidos = await unitofwork.Pedidos.GetAllAsync(pedidoOptions);
            var detalles = await unitofwork.DetallesPedido.GetAllAsync(detalleOptions);

            return pedidos
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
                        TotalGastado = g.Sum(p => p.Total),
                        UltimoPedido = g.Max(p => p.Fechapedido),
                        ProductoFavorito = productoFavorito
                    };
                })
                .OrderByDescending(c => c.TotalGastado)
                .ToList();
        }
    }
}