using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GastoController(IGastoService gastoService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AdicionarGastoAsync([FromBody] CreateGastoDTO createGastoDTO)
    {
        Gasto? gastoCriado = await gastoService.ValidarEAdicionarGastoAsync(createGastoDTO);
        return CreatedAtAction(nameof(GetGastoByIdEUsuarioId), new { gastoId = gastoCriado.Id, usuarioId = gastoCriado.UsuarioId }, gastoCriado); //trocar pra getById dps
    }

    [HttpGet]
    public async Task<IActionResult> ListarGastosPorUsuarioAsync([FromQuery] int usuarioId)
    {
        List<ListGastosDTO> gastos = await gastoService.GetAllGastosByUsuarioIdAsync(usuarioId);
        return Ok(gastos);
    }

    [HttpGet("{gastoId}/{usuarioId}")]
    public async Task<IActionResult> GetGastoByIdEUsuarioId(int gastoId, int usuarioId)
    {
        ListGastosDTO? gasto = await gastoService.GetGastoByIdEUsuarioId(gastoId, usuarioId);
        return Ok(gasto);
    }
}
