using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface IOrcamentoService
{
    Task<Orcamento> ValidarEAdicionarOrcamentoAsync(CreateOrcamentoDTO orcamentoDTO);
    Task<List<ListOrcamentoDTO>> GetAllOrcamentosByUsuarioIdAsync(int usuarioID);
}
