using Application.DTOs.Productos;
using Application.UseCases.Products.Categorias;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaProductoController(
    GetAllCategorias getAll,
    GetCategoriaById getById,
    CreateCategoria create,
    UpdateCategoria update,
    DeleteCategoria delete) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
        var categorias = await getAll.Execute();
        return Ok(categorias);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
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
    public async Task<IActionResult> Create([FromBody] CreateCategoriaProductoDto dto)
    {
        try
        {
            await create.Execute(dto);
            return Created();
        }
        catch (DomainException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriaProductoDto dto)
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