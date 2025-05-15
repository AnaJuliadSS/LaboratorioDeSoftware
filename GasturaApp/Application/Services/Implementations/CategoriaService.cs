using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;
using GasturaApp.Infrastructure.Mapper;

namespace GasturaApp.Application.Services.Implementations;

public class CategoriaService(ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository) : ICategoriaService
{
    public async Task<Categoria> ValidarEAdicionarCategoria(CreateCategoriaDTO createCategoriaDTO)
    {
        ArgumentNullException.ThrowIfNull(createCategoriaDTO);

        if (string.IsNullOrWhiteSpace(createCategoriaDTO.Descricao))
            throw new CampoObrigatorioException("descrição");

        if (createCategoriaDTO.UsuarioId <= 0)
            throw new CampoInvalidoException("Usuário ID");

        if (!await usuarioRepository.UsuarioExisteAsync(createCategoriaDTO.UsuarioId))
            throw new EntidadeNaoEncontradaException("Usuário");

        if (await categoriaRepository.CategoriaExisteParaUsuario(createCategoriaDTO.Descricao, createCategoriaDTO.UsuarioId))
            throw new CategoriaJaExisteParaUsuarioException(createCategoriaDTO.Descricao, createCategoriaDTO.UsuarioId);

        createCategoriaDTO.Descricao = createCategoriaDTO.Descricao.Trim();

        Categoria categoria = Mapper.Map<Categoria>(createCategoriaDTO);

        return await categoriaRepository.AdicionarCategoriaAsync(categoria);
    }

    public async Task<List<Categoria>> GetAllCategoriasByUsuarioIdAsync(int usuarioId)
    {
        if (usuarioId <= 0)
            throw new CampoInvalidoException("Usuário ID");

        if (!await usuarioRepository.UsuarioExisteAsync(usuarioId))
            throw new EntidadeNaoEncontradaException($"Usuário");

        return await categoriaRepository.GetAllCategoriasByUsuarioIdAsync(usuarioId);
    }

    public async Task<Categoria> GetCategoriaPorIdEUsuarioAsync(int categoriaId, int usuarioId)
    {
        var categoria = await categoriaRepository.GetCategoriaByIdEUsarioAsync(categoriaId, usuarioId);

        return categoria ?? throw new EntidadeNaoEncontradaException($"Categoria");
    }
}