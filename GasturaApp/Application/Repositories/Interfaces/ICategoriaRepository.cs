using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Repositories.Interfaces;

public interface ICategoriaRepository
{
    Task<Categoria> AdicionarCategoriaAsync(Categoria categoria);
    Task<List<Categoria>> GetAllCategoriasByUsuarioIdAsync(int usuarioId);
    Task<Categoria?> GetCategoriaByIdEUsarioAsync(int categoriaId, int usuarioId);
    Task<bool> CategoriaExisteParaUsuario(string descricaoCategoria, int usuarioId);
}
