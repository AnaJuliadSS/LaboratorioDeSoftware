using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Repositories.Interfaces;

public interface IGastoRepository
{
    Task<Gasto> AdicionarGastoAsync(Gasto gasto);
    Task<List<Gasto>> GetAllGastosByUsuarioIdAsync(int usuarioId);
    Task<Gasto?> GetGastoByIdEUsuarioId(int gastoId, int usuarioId);
    Task<bool> GastoPertenceAoUsuario(int gastoId, int usuarioId);
}
