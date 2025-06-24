using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GasturaApp.WebApi.Controllers;

[ApiController]
[Route("gastos")]
public class GastoController(IGastoService gastoService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AdicionarGastoAsync([FromBody] CreateGastoDTO createGastoDTO)
    {
        Gasto? gastoCriado = await gastoService.ValidarEAdicionarGastoAsync(createGastoDTO);
        return CreatedAtAction(nameof(GetGastoByIdEUsuarioId), new { gastoId = gastoCriado.Id, usuarioId = gastoCriado.UsuarioId }, gastoCriado); //trocar pra getById dps
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> ListarGastosPorUsuarioAsync(int usuarioId)
    {
        List<ListGastosDTO> gastos = await gastoService.GetAllGastosByUsuarioIdAsync(usuarioId);
        return Ok(gastos);
    }

    [HttpGet("{gastoId}/{usuarioId}")]
    public async Task<IActionResult> GetGastoByIdEUsuarioId(int gastoId, int usuarioId)
    {
        ListGastosDTO? gasto = await gastoService.GetGastoByIdEUsuarioIdAsync(gastoId, usuarioId);
        return Ok(gasto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarGastoAsync(int id, [FromBody] EditGastoDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var gastoAtualizado = await gastoService.EditarGastoByIdAsync(id, dto);
        return Ok(gastoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirGastoAsync(int id, [FromQuery] int usuarioId)
    {
        await gastoService.ExcluirGastoByIdAsync(id, usuarioId);
        return NoContent();
    }
}
