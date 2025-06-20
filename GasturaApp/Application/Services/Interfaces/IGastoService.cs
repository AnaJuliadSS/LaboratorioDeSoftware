using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface IGastoService
{
    Task<Gasto> ValidarEAdicionarGastoAsync(CreateGastoDTO gastoDTO);
    Task<List<ListGastosDTO>> GetAllGastosByUsuarioIdAsync (int usuarioId);
    Task<ListGastosDTO?> GetGastoByIdEUsuarioIdAsync(int gastoId, int usuarioId);
    Task<Gasto> EditarGastoByIdAsync(int gastoId, EditGastoDTO gasto);
    Task<bool> ExcluirGastoByIdAsync(int gastoId, int usuarioId);
}
