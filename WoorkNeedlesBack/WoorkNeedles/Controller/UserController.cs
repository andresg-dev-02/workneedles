using Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ListarUsuariosUseCase _listarUsuariosUseCase;

    public UserController(ListarUsuariosUseCase listarUsuariosUseCase)
    {
        _listarUsuariosUseCase = listarUsuariosUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var usuarios = await _listarUsuariosUseCase.ExecuteAsync();
        return Ok(usuarios);
    }
}