using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Data;

namespace GasturaApp.Application.Repositories.Implementations;

public class CategoriaRepository(AppDbContext context) : ICategoriaRepository
{
    public async Task<Categoria> AdicionarCategoriaAsync(Categoria categoria)
    {
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();
        return categoria;
    }
}
