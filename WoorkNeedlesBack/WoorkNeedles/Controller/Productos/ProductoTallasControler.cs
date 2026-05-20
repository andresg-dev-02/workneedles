using Application.DTOs.Productos;
using Application.UseCases.Products.Tallas;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WoorkNeedles.Controller.Productos
{
    [ApiController]
    [Route("api/Producto/{idProducto}/Talla")]
    public class ProductoTallasControler(
        GetAllProductoTallas getAll,
        CreateProductoTalla create,
        UpdateProductoTalla update,
        DeleteProductoTalla delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idProducto)
        {
            var talla = await getAll.Execute(idProducto);
            return Ok(talla);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idProducto, [FromBody] CreateProductoTallaDto dto)
        {
            try
            {
                dto.Idproducto = idProducto;
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int idProducto, int id, [FromBody] UpdateProductoTallaDto dto)
        {
            try
            {
                dto.Idproducto = idProducto;
                await update.Execute(id, dto);
                return Ok(new { message = "Talla actualizada del producto exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int idProducto, int id)
        {
            try
            {
                await delete.Execute(id);
                return Ok(new { message = "Talla eliminada del producto exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}