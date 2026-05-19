using Application.DTOs.Productos;
using Application.UseCases.Products.Colores;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WoorkNeedles.Controller.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class TallaController(
        GetAllTallas getAll,
        GetTallaById getById,
        CreateTalla create,
        UpdateTalla update,
        DeleteTalla delete
    ) : ControllerBase
    {
        [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tallas = await getAll.Execute();
        return Ok(tallas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var talla = await getById.Execute(id);
            return Ok(talla);
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTallaDto dto)
    {
        try
        {
            await create.Execute(dto);
            return Created();
        }
        catch (DomainException ex) { return BadRequest(ex.Message); }
    }
    }
}