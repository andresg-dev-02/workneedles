using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Application.DTOs.Pedido;
using Application.UseCases.Pedidos;
using Microsoft.AspNetCore.Authorization;

namespace WoorkNeedles.Controller.Pedidos
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController(
        GetAllPedidos getAll,
        GetPedidoById getById,
        CreatePedido create,
        UpdatePedido update,
        CambiarEstadoPedido cambiarEstado,
        DeletePedido delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await getAll.Execute();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pedido = await getById.Execute(id);
                return Ok(pedido);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePedidoDto dto)
        {
            try
            {
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePedidoDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Pedido actualizado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}/confirmar-entrega")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmarEntrega(int id, [FromQuery] string token)
        {
            try
            {
                await confirmarEntrega.Execute(id, token);
                return Redirect("http://localhost:4200/entrega-confirmada");
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoPedidoDto dto)
        {
            try
            {
                await cambiarEstado.Execute(id, dto);
                return Ok(new { message = "Estado actualizado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await delete.Execute(id);
                return Ok(new { message = "Pedido eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}