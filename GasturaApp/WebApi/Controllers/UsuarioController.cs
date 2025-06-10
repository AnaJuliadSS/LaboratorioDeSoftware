using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AdicionarUsuarioAsync([FromBody] CreateUsuarioDTO createUsuarioDTO)
    {
        Usuario? usuarioCriado = await usuarioService.ValidarEAdicionarUsuarioAsync(createUsuarioDTO);
        return CreatedAtAction(nameof(GetUsuarioByIdAsync), new { id = usuarioCriado.Id }, usuarioCriado);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsuariosAsync()
    {
        List<ListUsuariosDTO> usuarios = await usuarioService.GetAllUsuariosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioByIdAsync(int id)
    {
        Usuario? usuario = await usuarioService.GetUsuarioByIdAsync(id);
        return Ok(usuario);
    }
}
