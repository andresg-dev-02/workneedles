using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Reportes;
using Application.UseCases.Reportes;

namespace WoorkNeedles.Controller.Reportes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController(
        GetProductosMasVendidos productosMasVendidos,
        GetIngresosMensuales ingresosMensuales,
        GetFrecuenciaPedidos frecuenciaPedidos,
        GetComportamientoClientes comportamientoClientes
    ) : ControllerBase
    {
        [HttpGet("productos-mas-vendidos")]
        public async Task<IActionResult> ProductosMasVendidos()
        {
            var resultado = await productosMasVendidos.Execute();
            return Ok(resultado);
        }

        [HttpGet("ingresos-mensuales")]
        public async Task<IActionResult> IngresosMensuales()
        {
            var resultado = await ingresosMensuales.Execute();
            return Ok(resultado);
        }

        [HttpGet("frecuencia-pedidos")]
        public async Task<IActionResult> FrecuenciaPedidos()
        {
            var resultado = await frecuenciaPedidos.Execute();
            return Ok(resultado);
        }

        [HttpGet("comportamiento-clientes")]
        public async Task<IActionResult> ComportamientoClientes()
        {
            var resultado = await comportamientoClientes.Execute();
            return Ok(resultado);
        }
    }
}