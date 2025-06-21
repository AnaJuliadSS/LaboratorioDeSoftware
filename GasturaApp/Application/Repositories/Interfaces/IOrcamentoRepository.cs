using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Repositories.Interfaces;

public interface IOrcamentoRepository
{
    Task<Orcamento> AdicionarOrcamentoAsync(Orcamento orcamento);
    Task<List<Orcamento>> GetAllOrcamentosByUsuarioIdAsync(int id);
    Task<Orcamento> EditarOrcamentoByIdAsync(Orcamento orcamentoExistente, EditOrcamentoDTO orcamentoAtualizado);
    Task<Orcamento> GetOrcamentoByIdEUsuarioId(int orcamentoId, int usuarioId);
    Task<bool> ExcluirOrcamentoAsync(Orcamento orcamento);
}
