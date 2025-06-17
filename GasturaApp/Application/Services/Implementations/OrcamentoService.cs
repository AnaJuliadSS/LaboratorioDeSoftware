using GasturaApp.Application.Helpers.Mapper;
using GasturaApp.Application.Repositories.Implementations;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaApp.Application.Services.Implementations;

public class OrcamentoService(IOrcamentoRepository orcamentoRepository, IUsuarioRepository usuarioRepository, ICategoriaRepository categoriaRepository) : IOrcamentoService
{
    public async Task<List<ListOrcamentoDTO>> GetAllOrcamentosByUsuarioIdAsync(int usuarioID)
    {
        List<Orcamento> orcamentos = await orcamentoRepository.GetAllOrcamentosByUsuarioIdAsync(usuarioID);

        return orcamentos.Select(u => Mapper.Map<ListOrcamentoDTO>(u)).ToList();
    }

    public async Task<Orcamento> ValidarEAdicionarOrcamentoAsync(CreateOrcamentoDTO orcamentoDTO)
    {
        #region validações 
        if (orcamentoDTO.UsuarioId <= 0)
            throw new CampoInvalidoException("Usuário ID");

        if (!await usuarioRepository.UsuarioExisteAsync(orcamentoDTO.UsuarioId))
            throw new EntidadeNaoEncontradaException("Usuário");

        if (orcamentoDTO.CategoriaId <= 0)
            throw new CampoInvalidoException("Categoria ID");

        _ = await categoriaRepository.GetCategoriaByIdEUsarioAsync(orcamentoDTO.CategoriaId, orcamentoDTO.UsuarioId) ?? throw new EntidadeNaoEncontradaException("Categoria");

        if (orcamentoDTO.ValorLimite < 0)
            throw new CampoInvalidoException("Valor Limite");

        if (orcamentoDTO.MesReferencia == default)
            throw new CampoInvalidoException("Mês de Referência");
        #endregion

        Orcamento orcamento = Mapper.Map<Orcamento>(orcamentoDTO);
        return await orcamentoRepository.AdicionarOrcamentoAsync(orcamento);
    }
}
