using GasturaApp.Application.Helpers.Mapper;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Core.Enums;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaApp.Application.Services.Implementations;

public class GastoService(IGastoRepository gastoRepository,
                    ICategoriaRepository categoriaRepository,
                    IUsuarioRepository usuarioRepository) : IGastoService
{
    public async Task<List<ListGastosDTO>> GetAllGastosByUsuarioIdAsync(int usuarioId)
    {
        #region Validações
        if (!await usuarioRepository.UsuarioExisteAsync(usuarioId))
            throw new EntidadeNaoEncontradaException($"Usuário");
        #endregion 

        List<Gasto> gastos = await gastoRepository.GetAllGastosByUsuarioIdAsync(usuarioId);

        return gastos.Select(u => Mapper.Map<ListGastosDTO>(u)).ToList();
    }

    public async Task<ListGastosDTO?> GetGastoByIdEUsuarioId(int gastoId, int usuarioId)
    {
        if (!await usuarioRepository.UsuarioExisteAsync(usuarioId))
            throw new EntidadeNaoEncontradaException("Usuário");

        if (!await gastoRepository.GastoPertenceAoUsuario(gastoId, usuarioId))
            throw new EntidadeNaoEncontradaException("Gasto");

        Gasto gasto = await gastoRepository.GetGastoByIdEUsuarioId(gastoId, usuarioId) ?? throw new EntidadeNaoEncontradaException($"Categoria");
        
        ListGastosDTO gastoDto = Mapper.Map<ListGastosDTO>(gasto);

        return gastoDto;
    }

    public async Task<Gasto> ValidarEAdicionarGastoAsync(CreateGastoDTO gastoDTO)
    {
        #region Validações
        if (!await usuarioRepository.UsuarioExisteAsync(gastoDTO.UsuarioId))
             throw new EntidadeNaoEncontradaException("Usuário");

        //fazer igual de cima
        _ = await categoriaRepository.GetCategoriaByIdEUsarioAsync(gastoDTO.CategoriaId, gastoDTO.UsuarioId) ?? throw new EntidadeNaoEncontradaException("Categoria");

        if (!Enum.IsDefined(typeof(ModalidadePagamento), gastoDTO.ModalidadePagamento))
            throw new CampoInvalidoException("ModalidadePagamento");
        #endregion

        Gasto novoGasto = Mapper.Map<Gasto>(gastoDTO);
        return await gastoRepository.AdicionarGastoAsync(novoGasto);
    }
}
