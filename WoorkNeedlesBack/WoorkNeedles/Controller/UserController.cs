using Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.UserModel;
using Domain.Exceptions;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUsers _listarUsuariosUseCase;
    private readonly GetUserById _traerUusario;
    private readonly DeleteUser _eliminarUusario;
    private readonly AddUser _createUserUseCase;
    private readonly UpdateUser _updateUserUseCase;

    public UserController(GetAllUsers listarUsuariosUseCase, GetUserById traerUusario, DeleteUser eliminarUusario, AddUser createUserUseCase, UpdateUser updateUserUseCase)
    {
        _listarUsuariosUseCase = listarUsuariosUseCase;
        _traerUusario = traerUusario;
        _eliminarUusario = eliminarUusario;
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _listarUsuariosUseCase.Execute();
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

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUsuarioDto usuario)
    {
        await _createUserUseCase.Execute(usuario);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUsuarioDto actualizaruserdto)
    {
        try
        {
            await _updateUserUseCase.Execute(id, actualizaruserdto);
            return Ok(new { message = "Usuario actualizado exitosamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DomainException ex)
    {
        return BadRequest(ex.Message);
    }
    }

}