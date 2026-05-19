using Application.DTOs.Productos;
using Application.UseCases.Products.Colores;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WoorkNeedles.Controller.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController(
        GetAllColores getAll,
        GetColoreById getById,
        CreateColor create,
        UpdateColore update,
        DeleteColore delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var colores = await getAll.Execute();
            return Ok(colores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var color = await getById.Execute(id);
                return Ok(color);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateColorDto dto)
        {
            try
            {
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateColorDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Color actualizado exitosamente." });
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
                return Ok(new { message = "Color eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

    }
}