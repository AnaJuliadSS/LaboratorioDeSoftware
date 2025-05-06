using GasturaApp.Application.Repositories.Interfaces;
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
        //TODO: fazer middleware que trata as exceções globalmente para não ficar tanto catch
        try
        {
            var novaCategoria = await categoriaService.ValidarEAdicionarCategoria(createCategoriaDTO);

            return CreatedAtAction(nameof(GetCategoriaById), new { id = novaCategoria.Id }, novaCategoria);
        }
        catch (Exception ex) when (ex is CampoObrigatorioException || ex is CampoInvalidoException)
        {
            return BadRequest(ex.Message);
        }   
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoriaById(int categoriaId) => Ok(); //ainda não implementado
}
