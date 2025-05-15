using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface ICategoriaService
{
    Task<Categoria> ValidarEAdicionarCategoria(CreateCategoriaDTO categoria);
    Task<List<Categoria>> GetAllCategoriasByUsuarioIdAsync(int usuarioId);
    Task<Categoria> GetCategoriaPorIdEUsuarioAsync(int categoriaId, int usuarioId);
}
