using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Repositories.Interfaces;

public interface ICategoriaRepository
{
    Task<Categoria> AdicionarCategoriaAsync(Categoria categoria);
}
