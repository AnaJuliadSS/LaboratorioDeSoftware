using GasturaApp.Application.Helpers.Mapper;
using GasturaApp.Application.Repositories.Implementations;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaApp.Application.Services.Implementations;

public class CategoriaService(ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository) : ICategoriaService
{
    public async Task<Categoria> ValidarEAdicionarCategoriaAsync(CreateCategoriaDTO createCategoriaDTO)
    {
        #region validações
        ArgumentNullException.ThrowIfNull(createCategoriaDTO);

        if (string.IsNullOrWhiteSpace(createCategoriaDTO.Descricao))
            throw new CampoObrigatorioException("descrição");

        if (createCategoriaDTO.UsuarioId <= 0)
            throw new CampoInvalidoException("Usuário ID");

        if (!await usuarioRepository.UsuarioExisteAsync(createCategoriaDTO.UsuarioId))
            throw new EntidadeNaoEncontradaException("Usuário");

        if (!await categoriaRepository.CategoriaExisteParaUsuario(createCategoriaDTO.Descricao, createCategoriaDTO.UsuarioId))
            throw new CategoriaJaExisteParaUsuarioException(createCategoriaDTO.Descricao, createCategoriaDTO.UsuarioId);

        #endregion

        createCategoriaDTO.Descricao = createCategoriaDTO.Descricao.Trim();

        Categoria categoria = Mapper.Map<Categoria>(createCategoriaDTO);

        return await categoriaRepository.AdicionarCategoriaAsync(categoria);
    }

    public async Task<List<Categoria>> GetAllCategoriasByUsuarioIdAsync(int usuarioId)
    {
        #region validações
        if (!await usuarioRepository.UsuarioExisteAsync(usuarioId))
            throw new EntidadeNaoEncontradaException($"Usuário");
        #endregion

        return await categoriaRepository.GetAllCategoriasByUsuarioIdAsync(usuarioId);
    }

    public async Task<Categoria> GetCategoriaPorIdEUsuarioAsync(int categoriaId, int usuarioId)
    {
        var categoria = await categoriaRepository.GetCategoriaByIdEUsarioAsync(categoriaId, usuarioId);

        return categoria ?? throw new EntidadeNaoEncontradaException($"Categoria");
    }

    public async Task<bool> ExcluirCategoriaByIdAsync(int categoriaId, int usuarioId)
    {
        var categoria = await categoriaRepository.GetCategoriaByIdEUsarioAsync(categoriaId, usuarioId);

        return categoria == null ? throw new EntidadeNaoEncontradaException("Categoria") : await categoriaRepository.ExcluirCategoriaAsync(categoria);
    }
}