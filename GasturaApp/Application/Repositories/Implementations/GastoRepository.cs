using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.DTOs;
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

    public async Task<Gasto> EditarGastoByIdAsync(Gasto gastoExistente, EditGastoDTO dto)
    {
        if (dto.Valor.HasValue)
            gastoExistente.Valor = dto.Valor.Value;

        if (dto.ModalidadePagamento.HasValue)
            gastoExistente.ModalidadePagamento = dto.ModalidadePagamento.Value;

        if (dto.CategoriaId.HasValue)
            gastoExistente.CategoriaId = dto.CategoriaId.Value;

        if (dto.DataHora.HasValue)
            gastoExistente.DataHora = dto.DataHora.Value;

        if (!string.IsNullOrWhiteSpace(dto.Descricao))
            gastoExistente.Descricao = dto.Descricao;

        await context.SaveChangesAsync();

        return gastoExistente;
    }

    public async Task<bool> ExcluirGastoAsync(Gasto gasto)
    {
        context.Gastos.Remove(gasto);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> GastoPertenceAoUsuario(int gastoId, int usuarioId)
    {
        Gasto? gasto = await context.Gastos.FirstOrDefaultAsync(g => g.Id == gastoId && g.UsuarioId == usuarioId);
        return gasto == null;
    }

    public async Task<List<Gasto>> GetAllGastosByUsuarioIdAsync(int usuarioId)
    {
        return await context.Gastos
            .Include(g => g.Categoria)
            .Where(gasto => gasto.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Gasto?> GetGastoByIdEUsuarioId(int gastoId, int usuarioId)
    {
        return await context.Gastos
        .FirstOrDefaultAsync(g => g.Id == gastoId && g.UsuarioId == usuarioId);
    }
}
