using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GasturaApp.Application.Repositories.Implementations;

public class OrcamentoRepository(AppDbContext context) : IOrcamentoRepository
{
    public async Task<Orcamento> AdicionarOrcamentoAsync(Orcamento orcamento)
    {
        context.Orcamentos.Add(orcamento);
        await context.SaveChangesAsync();
        return orcamento;
    }

    public async Task<List<Orcamento>> GetAllOrcamentosByUsuarioIdAsync(int usuarioId)
    {
        return await context.Orcamentos
            .Include(o => o.Categoria)
            .Where(o => o.UsuarioId == usuarioId)
            .ToListAsync();
    }
}
