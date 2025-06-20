using Moq;
using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Core.Enums;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaAppTest.ServiceTests;
public class GastoServiceTest
{
    private readonly Mock<IGastoRepository> gastoRepositoryMock;
    private readonly Mock<ICategoriaRepository> categoriaRepositoryMock;
    private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
    private readonly GastoService gastoService;

    public GastoServiceTest()
    {
        gastoRepositoryMock = new Mock<IGastoRepository>();
        categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        gastoService = new GastoService(gastoRepositoryMock.Object, categoriaRepositoryMock.Object, usuarioRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllGastosByUsuarioIdAsync_Deve_Retornar_Gastos_Quando_Usuario_Existe()
    {
        int usuarioId = 1;
        var gastos = new List<Gasto>
        {
            new Gasto { Id = 1, UsuarioId = usuarioId, Descricao = "Gasto 1" },
            new Gasto { Id = 2, UsuarioId = usuarioId, Descricao = "Gasto 2" }
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(true);
        gastoRepositoryMock.Setup(g => g.GetAllGastosByUsuarioIdAsync(usuarioId)).ReturnsAsync(gastos);

        var resultado = await gastoService.GetAllGastosByUsuarioIdAsync(usuarioId);

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task GetAllGastosByUsuarioIdAsync_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Usuario_Nao_Existe()
    {
        int usuarioId = 1;
        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            gastoService.GetAllGastosByUsuarioIdAsync(usuarioId));
    }

    [Fact]
    public async Task GetGastoByIdEUsuarioId_Deve_Retornar_Gasto_Quando_Existe()
    {
        int gastoId = 1;
        int usuarioId = 1;
        var gasto = new Gasto { Id = gastoId, UsuarioId = usuarioId, Descricao = "Gasto" };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(true);
        gastoRepositoryMock.Setup(g => g.GastoPertenceAoUsuario(gastoId, usuarioId)).ReturnsAsync(true);
        gastoRepositoryMock.Setup(g => g.GetGastoByIdEUsuarioId(gastoId, usuarioId)).ReturnsAsync(gasto);

        var resultado = await gastoService.GetGastoByIdEUsuarioIdAsync(gastoId, usuarioId);

        Assert.NotNull(resultado);
        Assert.Equal(gastoId, resultado.Id);
        Assert.Equal(usuarioId, resultado.UsuarioId);
    }

    [Fact]
    public async Task GetGastoByIdEUsuarioId_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Usuario_Nao_Existe()
    {
        int gastoId = 1;
        int usuarioId = 1;

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            gastoService.GetGastoByIdEUsuarioIdAsync(gastoId, usuarioId));
    }

    [Fact]
    public async Task GetGastoByIdEUsuarioId_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Gasto_Nao_Pertence_Usuario()
    {
        int gastoId = 1;
        int usuarioId = 1;

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(true);
        gastoRepositoryMock.Setup(g => g.GastoPertenceAoUsuario(gastoId, usuarioId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            gastoService.GetGastoByIdEUsuarioIdAsync(gastoId, usuarioId));
    }

    [Fact]
    public async Task GetGastoByIdEUsuarioId_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Gasto_Nao_Encontrado()
    {
        int gastoId = 1;
        int usuarioId = 1;

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(true);
        gastoRepositoryMock.Setup(g => g.GastoPertenceAoUsuario(gastoId, usuarioId)).ReturnsAsync(true);
        gastoRepositoryMock.Setup(g => g.GetGastoByIdEUsuarioId(gastoId, usuarioId)).ReturnsAsync((Gasto)null!);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            gastoService.GetGastoByIdEUsuarioIdAsync(gastoId, usuarioId));
    }

    [Fact]
    public async Task ValidarEAdicionarGastoAsync_Deve_Criar_Gasto_Quando_Dados_Corretos()
    {
        var createDto = new CreateGastoDTO
        {
            Descricao = "Novo Gasto",
            UsuarioId = 1,
            CategoriaId = 2,
            ModalidadePagamento = ModalidadePagamento.Dinheiro
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(createDto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(c => c.GetCategoriaByIdEUsarioAsync(createDto.CategoriaId, createDto.UsuarioId)).ReturnsAsync(new Categoria());
        gastoRepositoryMock.Setup(g => g.AdicionarGastoAsync(It.IsAny<Gasto>())).ReturnsAsync((Gasto g) => g);

        var gastoCriado = await gastoService.ValidarEAdicionarGastoAsync(createDto);

        Assert.NotNull(gastoCriado);
        Assert.Equal(createDto.Descricao, gastoCriado.Descricao);
        Assert.Equal(createDto.UsuarioId, gastoCriado.UsuarioId);
        Assert.Equal(createDto.CategoriaId, gastoCriado.CategoriaId);
        Assert.Equal(createDto.ModalidadePagamento, gastoCriado.ModalidadePagamento);
    }

    [Fact]
    public async Task ValidarEAdicionarGastoAsync_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Usuario_Nao_Existe()
    {
        var createDto = new CreateGastoDTO
        {
            Descricao = "Gasto",
            UsuarioId = 1,
            CategoriaId = 1,
            ModalidadePagamento = ModalidadePagamento.Credito
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(createDto.UsuarioId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            gastoService.ValidarEAdicionarGastoAsync(createDto));
    }

    [Fact]
    public async Task ValidarEAdicionarGastoAsync_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Categoria_Nao_Existe()
    {
        var createDto = new CreateGastoDTO
        {
            Descricao = "Gasto",
            UsuarioId = 1,
            CategoriaId = 1,
            ModalidadePagamento = ModalidadePagamento.Credito
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(createDto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(c => c.GetCategoriaByIdEUsarioAsync(createDto.CategoriaId, createDto.UsuarioId)).ReturnsAsync((Categoria)null!);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            gastoService.ValidarEAdicionarGastoAsync(createDto));
    }

    [Theory]
    [InlineData(-1)] // valor inválido que não faz parte do enum
    [InlineData(100)] // valor inválido que não faz parte do enum
    public async Task ValidarEAdicionarGastoAsync_Deve_Lancar_CampoInvalidoException_Quando_ModalidadePagamento_Invalida(int modalidadeInvalida)
    {
        var createDto = new CreateGastoDTO
        {
            Descricao = "Gasto",
            UsuarioId = 1,
            CategoriaId = 1,
            ModalidadePagamento = (ModalidadePagamento)modalidadeInvalida
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(createDto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(c => c.GetCategoriaByIdEUsarioAsync(createDto.CategoriaId, createDto.UsuarioId)).ReturnsAsync(new Categoria());

        await Assert.ThrowsAsync<CampoInvalidoException>(() =>
            gastoService.ValidarEAdicionarGastoAsync(createDto));
    }
}
