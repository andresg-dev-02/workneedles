using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Application.DTOs.Pago;
using Application.UseCases.Pago;

namespace WoorkNeedles.Controller.Pagos
{
    [ApiController]
    [Route("api/Pedido/{idPedido}/Pagos")]
    public class PagoController(
        GetAllPagos getAll,
        CreatePago create,
        CambiarEstadoPago cambiarEstado,
        DeletePago delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idPedido)
        {
            var pagos = await getAll.Execute(idPedido);
            return Ok(pagos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idPedido, [FromBody] CreatePagoDto dto)
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
        public async Task<IActionResult> CambiarEstado(int idPedido, int id, [FromBody] CambiarEstadoPagoDto dto)
        {
            try
            {
                await cambiarEstado.Execute(id, dto);
                return Ok(new { message = "Estado de pago actualizado exitosamente." });
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
                return Ok(new { message = "Pago eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}