using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrcamentoController(IOrcamentoService orcamentoService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AdicionarOrcamentoAsync([FromBody] CreateOrcamentoDTO createOrcamentoDTO)
    {
        Orcamento? orcamentoCriado = await orcamentoService.ValidarEAdicionarOrcamentoAsync(createOrcamentoDTO);
        return Ok(orcamentoCriado); 
    }

    [HttpGet]
    public async Task<IActionResult> ListarOrcamentosPorUsuarioAsync([FromQuery] int usuarioId)
    {
        List<ListOrcamentoDTO> orcamentosDTO = await orcamentoService.GetAllOrcamentosByUsuarioIdAsync(usuarioId);
        return Ok(orcamentosDTO);
    }
}
