using Application.DTOs.Productos;
using Application.UseCases.Products.Inventario;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WoorkNeedles.Controller.Productos
{
    [ApiController]
    [Route("api/Producto/{idProducto}/Inventario")]
    public class InventarioController(
        GetAllInventario getAll,
        UpdateInventario update,
        CreateInventario create,
        DeleteInventario delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idProducto)
        {
            var inventario = await getAll.Execute(idProducto);
            return Ok(inventario);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idProducto, [FromBody] CreateInventarioDto dto)
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
        public async Task<IActionResult> Update(int idProducto, int id, [FromBody] UpdateInventarioDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Stock actualizado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (DomainException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int idProducto, int id)
        {
            try
            {
                await delete.Execute(id);
                return Ok(new { message = "Registro de inventario eliminado exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
    
}