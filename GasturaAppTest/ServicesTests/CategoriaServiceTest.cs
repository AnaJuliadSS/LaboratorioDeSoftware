using Moq;
using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaAppTest.ServiceTests;
public class CategoriaServiceTest
{
    private readonly Mock<ICategoriaRepository> categoriaRepositoryMock;
    private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
    private readonly CategoriaService categoriaService;

    public CategoriaServiceTest()
    {
        categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        categoriaService = new CategoriaService(categoriaRepositoryMock.Object, usuarioRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidarEAdicionarCategoriaAsync_Deve_Criar_Categoria_Quando_Dados_Corretos()
    {
        var createDto = new CreateCategoriaDTO
        {
            Descricao = "Nova Categoria",
            UsuarioId = 1
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(createDto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(c => c.CategoriaExisteParaUsuario(createDto.Descricao, createDto.UsuarioId)).ReturnsAsync(false);
        categoriaRepositoryMock.Setup(c => c.AdicionarCategoriaAsync(It.IsAny<Categoria>())).ReturnsAsync((Categoria cat) => cat);

        var categoriaCriada = await categoriaService.ValidarEAdicionarCategoriaAsync(createDto);

        Assert.NotNull(categoriaCriada);
        Assert.Equal(createDto.Descricao.Trim(), categoriaCriada.Descricao);
        Assert.Equal(createDto.UsuarioId, categoriaCriada.UsuarioId);
    }

    [Fact]
    public async Task ValidarEAdicionarCategoriaAsync_Deve_Lancar_ArgumentNullException_Quando_Dto_Nulo()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            categoriaService.ValidarEAdicionarCategoriaAsync(null!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidarEAdicionarCategoriaAsync_Deve_Lancar_CampoObrigatorioException_Quando_Descricao_Invalida(string descricaoInvalida)
    {
        var dto = new CreateCategoriaDTO { Descricao = descricaoInvalida, UsuarioId = 1 };

        await Assert.ThrowsAsync<CampoObrigatorioException>(() =>
            categoriaService.ValidarEAdicionarCategoriaAsync(dto));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ValidarEAdicionarCategoriaAsync_Deve_Lancar_CampoInvalidoException_Quando_UsuarioId_Invalido(int usuarioIdInvalido)
    {
        var dto = new CreateCategoriaDTO { Descricao = "Descrição", UsuarioId = usuarioIdInvalido };

        await Assert.ThrowsAsync<CampoInvalidoException>(() =>
            categoriaService.ValidarEAdicionarCategoriaAsync(dto));
    }

    [Fact]
    public async Task ValidarEAdicionarCategoriaAsync_Deve_Lancar_EntidadeNaoEncontradaException_Quando_UsuarioNaoExiste()
    {
        var dto = new CreateCategoriaDTO { Descricao = "Descrição", UsuarioId = 1 };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            categoriaService.ValidarEAdicionarCategoriaAsync(dto));
    }

    [Fact]
    public async Task ValidarEAdicionarCategoriaAsync_Deve_Lancar_CategoriaJaExisteParaUsuarioException_Quando_CategoriaJaExiste()
    {
        var dto = new CreateCategoriaDTO { Descricao = "Descrição", UsuarioId = 1 };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(c => c.CategoriaExisteParaUsuario(dto.Descricao, dto.UsuarioId)).ReturnsAsync(true);

        await Assert.ThrowsAsync<CategoriaJaExisteParaUsuarioException>(() =>
            categoriaService.ValidarEAdicionarCategoriaAsync(dto));
    }

    [Fact]
    public async Task GetAllCategoriasByUsuarioIdAsync_Deve_Retornar_Categorias_Quando_Usuario_Existe()
    {
        int usuarioId = 1;
        var categorias = new List<Categoria>
        {
            new Categoria { Id = 1, Descricao = "Cat1", UsuarioId = usuarioId },
            new Categoria { Id = 2, Descricao = "Cat2", UsuarioId = usuarioId }
        };

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(c => c.GetAllCategoriasByUsuarioIdAsync(usuarioId)).ReturnsAsync(categorias);

        var resultado = await categoriaService.GetAllCategoriasByUsuarioIdAsync(usuarioId);

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task GetAllCategoriasByUsuarioIdAsync_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Usuario_Nao_Existe()
    {
        int usuarioId = 1;

        usuarioRepositoryMock.Setup(u => u.UsuarioExisteAsync(usuarioId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            categoriaService.GetAllCategoriasByUsuarioIdAsync(usuarioId));
    }

    [Fact]
    public async Task GetCategoriaPorIdEUsuarioAsync_Deve_Retornar_Categoria_Quando_Existe()
    {
        int categoriaId = 1;
        int usuarioId = 1;
        var categoria = new Categoria { Id = categoriaId, Descricao = "Cat", UsuarioId = usuarioId };

        categoriaRepositoryMock.Setup(c => c.GetCategoriaByIdEUsarioAsync(categoriaId, usuarioId)).ReturnsAsync(categoria);

        var resultado = await categoriaService.GetCategoriaPorIdEUsuarioAsync(categoriaId, usuarioId);

        Assert.NotNull(resultado);
        Assert.Equal(categoriaId, resultado.Id);
        Assert.Equal(usuarioId, resultado.UsuarioId);
    }

    [Fact]
    public async Task GetCategoriaPorIdEUsuarioAsync_Deve_Lancar_EntidadeNaoEncontradaException_Quando_Nao_Existe()
    {
        int categoriaId = 1;
        int usuarioId = 1;

        categoriaRepositoryMock.Setup(c => c.GetCategoriaByIdEUsarioAsync(categoriaId, usuarioId)).ReturnsAsync((Categoria)null!);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            categoriaService.GetCategoriaPorIdEUsuarioAsync(categoriaId, usuarioId));
    }
}
