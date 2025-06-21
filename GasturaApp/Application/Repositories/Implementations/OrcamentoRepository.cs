using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.DTOs;
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

    public async Task<Orcamento> EditarOrcamentoByIdAsync(Orcamento orcamentoExistente, EditOrcamentoDTO dto)
    {
        if (dto.ValorLimite > 0 && orcamentoExistente.ValorLimite != dto.ValorLimite)
        {
            orcamentoExistente.ValorLimite = dto.ValorLimite;
        }

        if (dto.MesReferencia != default && orcamentoExistente.MesReferencia.Date != dto.MesReferencia.Date)
        {
            orcamentoExistente.MesReferencia = dto.MesReferencia;
        }

        if (dto.CategoriaId > 0 && orcamentoExistente.CategoriaId != dto.CategoriaId)
        {
            orcamentoExistente.CategoriaId = dto.CategoriaId;
        }

        if (dto.UsuarioId > 0 && orcamentoExistente.UsuarioId != dto.UsuarioId)
        {
            orcamentoExistente.UsuarioId = dto.UsuarioId;
        }

        await context.SaveChangesAsync();

        return orcamentoExistente;
    }

    public async Task<bool> ExcluirOrcamentoAsync(Orcamento orcamento)
    {
        context.Orcamentos.Remove(orcamento);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Orcamento>> GetAllOrcamentosByUsuarioIdAsync(int usuarioId)
    {
        return await context.Orcamentos
            .Include(o => o.Categoria)
            .Where(o => o.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Orcamento> GetOrcamentoByIdEUsuarioId(int orcamentoId, int usuarioId)
    {
        return await context.Orcamentos
         .FirstOrDefaultAsync(o => o.Id == orcamentoId && o.UsuarioId == usuarioId);
    }
}
