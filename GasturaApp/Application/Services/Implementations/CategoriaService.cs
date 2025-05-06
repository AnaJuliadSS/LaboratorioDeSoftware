using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaApp.Application.Services.Implementations;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
{
    public async Task<Categoria> ValidarEAdicionarCategoria(CreateCategoriaDTO createCategoriaDTO)
    {
        ArgumentNullException.ThrowIfNull(createCategoriaDTO);

        if (string.IsNullOrWhiteSpace(createCategoriaDTO.Descricao))
            throw new CampoObrigatorioException("descrição");

        if (createCategoriaDTO.UsuarioId <= 0)
            throw new CampoInvalidoException("Usuário ID");

        //TODO: adicionar verificação para caso de categoria já existir para esse usuário
        //TODO: adicionar verificação para caso de usuário id não existir no banco

        Categoria categoria = new()
        {
            Descricao = createCategoriaDTO.Descricao.Trim(),
            UsuarioId = createCategoriaDTO.UsuarioId
        };

        return await categoriaRepository.AdicionarCategoriaAsync(categoria);
    }
}
