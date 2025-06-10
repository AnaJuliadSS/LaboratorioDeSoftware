using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface IGastoService
{
    Task<Gasto> ValidarEAdicionarGastoAsync(CreateGastoDTO gastoDTO);
    Task<List<ListGastosDTO>> GetAllGastosByUsuarioIdAsync (int usuarioId);
    Task<ListGastosDTO?> GetGastoByIdEUsuarioId(int gastoId, int usuarioId);
}
