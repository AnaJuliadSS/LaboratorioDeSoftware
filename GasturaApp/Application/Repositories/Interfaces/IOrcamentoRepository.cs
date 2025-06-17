using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Repositories.Interfaces;

public interface IOrcamentoRepository
{
    Task<Orcamento> AdicionarOrcamentoAsync(Orcamento orcamento);
    Task<List<Orcamento>> GetAllOrcamentosByUsuarioIdAsync(int id);
}
