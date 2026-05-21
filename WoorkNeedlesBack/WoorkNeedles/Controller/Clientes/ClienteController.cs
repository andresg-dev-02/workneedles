using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Application.DTOs.Client;
using Application.UseCases.Clients;

namespace WoorkNeedles.Controller.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController(
        GetAllClientes getAll,
        GetClienteById getById,
        CreateCliente create,
        UpdateCliente update,
        DeleteCliente delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await getAll.Execute();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cliente = await getById.Execute(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteDto dto)
        {
            try
            {
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Cliente actualizado exitosamente." });
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
                return Ok(new { message = "Cliente eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}