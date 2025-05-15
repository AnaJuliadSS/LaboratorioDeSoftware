using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController(ICategoriaService categoriaService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCategoriaAsync([FromBody] CreateCategoriaDTO createCategoriaDTO)
    {
        var novaCategoria = await categoriaService.ValidarEAdicionarCategoria(createCategoriaDTO);

        return CreatedAtAction(nameof(GetCategoriaById), new { id = novaCategoria.Id }, novaCategoria);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoriaById(int id, [FromQuery] int usuarioId)
    {
        var categoria = await categoriaService.GetCategoriaPorIdEUsuarioAsync(id, usuarioId);
        return Ok(categoria);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriasAsync([FromQuery] int usuarioId)
    {
        var categorias = await categoriaService.GetAllCategoriasByUsuarioIdAsync(usuarioId);
        return Ok(categorias);
    }
}
