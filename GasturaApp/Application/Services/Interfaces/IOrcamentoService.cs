using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface IOrcamentoService
{
    Task<Orcamento> ValidarEAdicionarOrcamentoAsync(CreateOrcamentoDTO orcamentoDTO);
    Task<List<ListOrcamentoDTO>> GetAllOrcamentosByUsuarioIdAsync(int usuarioID);
    Task<Orcamento> EditarOrcamentoByIdAsync(int orcamentoId, EditOrcamentoDTO orcamentoDTO);
    Task<bool> ExcluirOrcamentoByIdAsync(int orcamentoId, int usuarioId);
}
