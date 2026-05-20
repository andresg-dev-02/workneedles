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
    public class CategoriaInsumoController(
        GetAllCategoriasInsumo getAll,
        GetCategoriaInsumoById getById,
        CreateCategoriaInsumo create,
        UpdateCategoriaInsumo update,
        DeleteCategoriaInsumo delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await getAll.Execute();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var categoria = await getById.Execute(id);
                return Ok(categoria);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaInsumoDto dto)
        {
            try
            {
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriaInsumoDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Categoría actualizada exitosamente." });
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
                return Ok(new { message = "Categoría eliminada exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}