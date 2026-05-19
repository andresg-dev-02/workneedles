using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Login;
using Application.DTOs.Login;
using Domain.Exceptions;

namespace WoorkNeedles.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(Auth loginCase) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto usuarioLoginDto)
        {
            try
            {
                var token = await loginCase.Execute(usuarioLoginDto);
                return Ok(token);
            }
            catch (DomainException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}