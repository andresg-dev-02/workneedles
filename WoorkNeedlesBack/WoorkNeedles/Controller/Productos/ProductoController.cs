using Application.DTOs.Productos;
using Application.UseCases.Products.Producto;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WoorkNeedles.Controller.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController(
        GetAllProductos getAll,
        GetProductoById getById,
        CreateProducto create,
        UpdateProducto update,
        DeleteProducto delete) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await getAll.Execute();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var producto = await getById.Execute(id);
                return Ok(producto);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductoDto dto)
        {
            try
            {
                await create.Execute(dto);
                return Created();
            }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductoDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Producto actualizado exitosamente." });
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
                return Ok(new { message = "Producto eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}