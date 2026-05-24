using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Geo;
using Application.DTOs.Geo;
using Domain.Exceptions;

namespace WoorkNeedles.Controller.Ubications
{
    [ApiController]
    [Route("api/[controller]")]
    public class UbicationController(
        GetAllUbications getAllUbications
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
            var ubicaciones = await getAllUbications.Execute();
            return Ok(ubicaciones);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}