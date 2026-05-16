using Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsers _listarUsuariosUseCase;
    private readonly GetUserById _traerUusario;
    private readonly DeleteUser _eliminarUusario;

    public UserController(GetAllUsers listarUsuariosUseCase, GetUserById traerUusario, DeleteUser eliminarUusario)
    {
        _listarUsuariosUseCase = listarUsuariosUseCase;
        _traerUusario = traerUusario;
        _eliminarUusario = eliminarUusario;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _listarUsuariosUseCase.TraerUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var usuario = await _traerUusario.Execute(id);
            return Ok(usuario);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            await _eliminarUusario.Execute(id);
            return Ok(new { message = "Usuario eliminado exitosamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}