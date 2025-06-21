using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GasturaApp.Application.Repositories.Implementations;

public class CategoriaRepository(AppDbContext context) : ICategoriaRepository
{
    public async Task<Categoria> AdicionarCategoriaAsync(Categoria categoria)
    {
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();
        return categoria;
    }

    public async Task<bool> CategoriaExisteParaUsuario(string descricaoCategoria, int usuarioId)
    {
        var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Descricao == descricaoCategoria && c.UsuarioId == usuarioId);
        return categoria == null;
    }

    public async Task<bool> ExcluirCategoriaAsync(Categoria categoria)
    {
        context.Categorias.Remove(categoria);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Categoria>> GetAllCategoriasByUsuarioIdAsync(int usuarioId)
    {
        return await context.Categorias
        .Where(c => c.UsuarioId == usuarioId)
        .ToListAsync();
    }

    public async Task<Categoria?> GetCategoriaByIdEUsarioAsync(int categoriaId, int usuarioId)
    {
        return await context.Categorias
        .FirstOrDefaultAsync(c => c.Id == categoriaId && c.UsuarioId == usuarioId);
    }
}
