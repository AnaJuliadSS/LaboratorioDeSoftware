using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GasturaApp.Application.Repositories.Implementations;

public class GastoRepository(AppDbContext context) : IGastoRepository

{
    public async Task<Gasto> AdicionarGastoAsync(Gasto gasto)
    {
        context.Gastos.Add(gasto);
        await context.SaveChangesAsync();
        return gasto;
    }

    public async Task<bool> GastoPertenceAoUsuario(int gastoId, int usuarioId)
    {
        Gasto? gasto = await context.Gastos.FirstOrDefaultAsync(g => g.Id == gastoId && g.UsuarioId == usuarioId);
        return gasto == null;
    }

    public async Task<List<Gasto>> GetAllGastosByUsuarioIdAsync(int usuarioId)
    {
        return await context.Gastos.Where(gasto => gasto.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<Gasto?> GetGastoByIdEUsuarioId(int gastoId, int usuarioId)
    {
        return await context.Gastos
        .FirstOrDefaultAsync(g => g.Id == gastoId && g.UsuarioId == usuarioId);
    }
}
