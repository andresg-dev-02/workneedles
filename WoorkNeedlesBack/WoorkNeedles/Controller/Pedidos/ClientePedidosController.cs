using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Application.DTOs.Pedido;
using Application.UseCases.Pedidos;

namespace WoorkNeedles.Controller.Pedidos
{
    [ApiController]
    [Route("api/Cliente/{idCliente}/Pedidos")]
    public class ClientePedidosController(GetPedidosByCliente getAll) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idCliente)
        {
            var pedidos = await getAll.Execute(idCliente);
            return Ok(pedidos);
        }
    }
}