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
    [Route("api/Pedido/{idPedido}/Detalles")]
    public class DetallePedidoController(
        GetAllDetallesPedido getAll,
        CreateDetallePedido create,
        UpdateDetallePedido update,
        DeleteDetallePedido delete) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idPedido)
        {
            var detalles = await getAll.Execute(idPedido);
            return Ok(detalles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idPedido, [FromBody] CreateDetallePedidoDto dto)
        {
            try
            {
                dto.Idpedido = idPedido;
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int idPedido, int id, [FromBody] UpdateDetallePedidoDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Detalle actualizado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int idPedido, int id)
        {
            try
            {
                await delete.Execute(id);
                return Ok(new { message = "Detalle eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}