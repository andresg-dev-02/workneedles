using Application.DTOs.Productos;
using Application.UseCases.Products.Colores;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WoorkNeedlesBack.Controller.Productos;

[ApiController]
[Route("api/Producto/{idProducto}/Colores")]
public class ProductoColoresController(
    GetAllProductoColor getAll,
    CreateProductoColor create,
    DeleteProductoColor delete) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int idProducto)
    {
        var colores = await getAll.Execute(idProducto);
        return Ok(colores);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int idProducto, [FromBody] CreateProductoColorDto dto)
    {
        try
        {
            dto.Idproducto = idProducto;
            await create.Execute(dto);
            return Created();
        }
        catch (DomainException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int idProducto, int id)
    {
        try
        {
            await delete.Execute(id);
            return Ok(new { message = "Color eliminado del producto exitosamente." });
        }
        catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
    }
}
