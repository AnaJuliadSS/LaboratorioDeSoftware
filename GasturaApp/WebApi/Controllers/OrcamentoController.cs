using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;


[ApiController]
[Route("orcamentos")]
public class OrcamentoController(IOrcamentoService orcamentoService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AdicionarOrcamentoAsync([FromBody] CreateOrcamentoDTO createOrcamentoDTO)
    {
        Orcamento? orcamentoCriado = await orcamentoService.ValidarEAdicionarOrcamentoAsync(createOrcamentoDTO);
        return Ok(orcamentoCriado); 
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> ListarOrcamentosPorUsuarioAsync(int usuarioId)
    {
        List<ListOrcamentoDTO> orcamentosDTO = await orcamentoService.GetAllOrcamentosByUsuarioIdAsync(usuarioId);
        return Ok(orcamentosDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarOrcamentoAsync(int id, [FromBody] EditOrcamentoDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var orcamentoAtualizado = await orcamentoService.EditarOrcamentoByIdAsync(id, dto);
        return Ok(orcamentoAtualizado);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirOrcamentoAsync(int id, [FromQuery] int usuarioId)
    {
        await orcamentoService.ExcluirOrcamentoByIdAsync(id, usuarioId);
        return NoContent();
    }
}
