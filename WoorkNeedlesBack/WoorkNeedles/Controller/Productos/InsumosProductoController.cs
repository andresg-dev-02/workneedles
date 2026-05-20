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
    [Route("api/Producto/{idProducto}/Insumos")]
    public class InsumosProductoController(
        GetAllInsumosProducto getAll,
        CreateInsumoProducto create,
        UpdateInsumosProducto update,
        DeleteInsumosProducto delete
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idProducto)
        {
            var insumos = await getAll.Execute(idProducto);
            return Ok(insumos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idProducto, [FromBody] CreateInsumosProductoDto dto)
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
        public async Task<IActionResult> Update(int idProducto, int id, [FromBody] UpdateInsumosProductoDto dto)
        {
            try
            {
                await update.Execute(id, dto);
                return Ok(new { message = "Cantidad actualizada exitosamente." });
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
                return Ok(new { message = "Insumo eliminado del producto exitosamente." });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}