using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CadastrarUsuarioAsync([FromBody] CreateUsuarioDTO createUsuarioDTO)
    {
        var usuarioCriado = await usuarioService.CadastrarUsuarioAsync(createUsuarioDTO);
        return Created($"api/usuarios/{usuarioCriado.Id}", usuarioCriado);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsuariosAsync()
    {
        var usuarios = await usuarioService.GetAllUsuariosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioByIdAsync(int id)
    {
        var usuario = await usuarioService.GetUsuarioByIdAsync(id);
        return Ok(usuario);
    }
}
