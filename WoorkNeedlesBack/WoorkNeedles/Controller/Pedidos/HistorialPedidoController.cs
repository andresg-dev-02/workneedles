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
    [Route("api/Pedido/{idPedido}/Historial")]
    public class HistorialPedidoController(
        GetAllHistorialPedido getAll,
        CreateHistorialPedido create,
        DeleteHistorialPedido delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idPedido)
        {
            var historial = await getAll.Execute(idPedido);
            return Ok(historial);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idPedido, [FromBody] CreateHistorialPedidoDto dto)
        {
            try
            {
                dto.Idpedido = idPedido;
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int idPedido, int id)
        {
            try
            {
                await delete.Execute(id);
                return Ok(new { message = "Historial eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}