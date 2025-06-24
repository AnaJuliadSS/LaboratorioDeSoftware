using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("categorias")]
public class CategoriaController(ICategoriaService categoriaService) : ControllerBase
{

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("API online");
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarCategoriaAsync([FromBody] CreateCategoriaDTO createCategoriaDTO)
    {
        Categoria? novaCategoria = await categoriaService.ValidarEAdicionarCategoriaAsync(createCategoriaDTO);
        return CreatedAtAction(nameof(GetCategoriaById), new { categoriaId = novaCategoria.Id, usuarioId = novaCategoria.UsuarioId }, novaCategoria);
    }

    [HttpGet("{categoriaId}/{usuarioId}")]
    public async Task<IActionResult> GetCategoriaById(int categoriaId, int usuarioId)
    {
        Categoria? categoria = await categoriaService.GetCategoriaPorIdEUsuarioAsync(categoriaId, usuarioId);
        return Ok(categoria);
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> GetAllCategoriasAsync(int usuarioId)
    {
        List<Categoria> categorias = await categoriaService.GetAllCategoriasByUsuarioIdAsync(usuarioId);
        return Ok(categorias);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirCategoriaAsync(int id, [FromQuery] int usuarioId)
    {
        await categoriaService.ExcluirCategoriaByIdAsync(id, usuarioId);
        return NoContent();
    }
}
