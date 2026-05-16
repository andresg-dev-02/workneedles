using Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsers _listarUsuariosUseCase;

    public UserController(GetAllUsers listarUsuariosUseCase)
    {
        _listarUsuariosUseCase = listarUsuariosUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _listarUsuariosUseCase.TraerUsuarios();
        return Ok(usuarios);
    }
}