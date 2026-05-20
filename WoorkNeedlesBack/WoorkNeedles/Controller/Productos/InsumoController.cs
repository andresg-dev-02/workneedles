using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Application.DTOs.Productos;
using Application.UseCases.Products.Insumos;

namespace WoorkNeedles.Controller.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsumoController(
        GetAllInsumos getAll,
        GetInsumoById getById,
        CreateInsumo create,
        UpdateInsumo update,
        DeleteInsumo delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var insumos = await getAll.Execute();
            return Ok(insumos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var insumo = await getById.Execute(id);
                return Ok(insumo);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInsumoDto dto)
        {
            try
            {
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInsumoDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Insumo actualizado exitosamente." });
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
                return Ok(new { message = "Insumo eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}