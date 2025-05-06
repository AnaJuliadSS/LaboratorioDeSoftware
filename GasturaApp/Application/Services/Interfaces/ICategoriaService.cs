using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface ICategoriaService
{
    Task<Categoria> ValidarEAdicionarCategoria(CreateCategoriaDTO categoria);
}
