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
    [Route("api/Pedido/{idPedido}/Devoluciones")]
    public class DevolucionesController(
        GetAllDevoluciones getAll,
        CreateDevolucione create,
        CambiarEstadoDevolucione cambiarEstado,
        DeleteDevolucione delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idPedido)
        {
            var devoluciones = await getAll.Execute(idPedido);
            return Ok(devoluciones);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idPedido, [FromBody] CreateDevolucioneDto dto)
        {
            try
            {
                dto.Idpedido = idPedido;
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int idPedido, int id, [FromBody] CambiarEstadoDevolucioneDto dto)
        {
            try
            {
                await cambiarEstado.Execute(id, dto);
                return Ok(new { message = "Estado de devolución actualizado exitosamente." });
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
                return Ok(new { message = "Devolución eliminada exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}