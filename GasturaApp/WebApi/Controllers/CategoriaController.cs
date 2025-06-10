using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController(ICategoriaService categoriaService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AdicionarCategoriaAsync([FromBody] CreateCategoriaDTO createCategoriaDTO)
    {
        Categoria? novaCategoria = await categoriaService.ValidarEAdicionarCategoriaAsync(createCategoriaDTO);
        return CreatedAtAction(nameof(GetCategoriaById), new { id = novaCategoria.Id, usuarioId = novaCategoria.UsuarioId }, novaCategoria);
    }

    [HttpGet("{categoriaId}/{usuarioId}")]
    public async Task<IActionResult> GetCategoriaById(int categoriaId, int usuarioId)
    {
        Categoria? categoria = await categoriaService.GetCategoriaPorIdEUsuarioAsync(categoriaId, usuarioId);
        return Ok(categoria);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriasAsync([FromQuery] int usuarioId)
    {
        List<Categoria> categorias = await categoriaService.GetAllCategoriasByUsuarioIdAsync(usuarioId);
        return Ok(categorias);
    }
}
