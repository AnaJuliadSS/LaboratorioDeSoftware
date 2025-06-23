using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Implementations;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;
using Moq;

namespace GasturaAppTest.ServiceTests;

public class UsuarioServiceTest
{
    private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
    private readonly UsuarioService usuarioService;

    public UsuarioServiceTest()
    {
        usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        usuarioService = new UsuarioService(usuarioRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidarEAdicionarUsuarioAsync_Deve_Criar_Usuario_Quando_Dados_Corretos()
    {
        // Arrange
        var createDto = new CreateUsuarioDTO
        {
            Nome = "Ana Julia",
            Email = "ana.julia@example.com",
            Senha = "senhaAdequada34.*"
        };

        usuarioRepositoryMock.Setup(r => r.ExisteEmailAsync(createDto.Email))
            .ReturnsAsync(false);

        usuarioRepositoryMock.Setup(r => r.AdicionarUsuarioAsync(It.IsAny<Usuario>()))
            .ReturnsAsync((Usuario u) => u);

        // Act
        var usuarioCriado = await usuarioService.ValidarEAdicionarUsuarioAsync(createDto);

        // Assert
        Assert.NotNull(usuarioCriado);
        Assert.Equal(createDto.Nome, usuarioCriado.Nome);
        Assert.Equal(createDto.Email, usuarioCriado.Email);
        Assert.NotNull(usuarioCriado.Senha);
    }

    [Fact]
    public async Task ValidarEAdicionarUsuarioAsync_Deve_Lancar_Excecao_Quando_Senha_Tiver_Sequencia()
    {
        // Arrange
        var createDto = new CreateUsuarioDTO
        {
            Nome = "Ana Julia",
            Email = "ana.julia@example.com",
            Senha = "12345678"
        };

        usuarioRepositoryMock.Setup(r => r.ExisteEmailAsync(createDto.Email))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<SenhaNaoDeveConterSequenciasException>(() =>
            usuarioService.ValidarEAdicionarUsuarioAsync(createDto));
    }

    [Fact]
    public async Task GetAllUsuariosAsync_Deve_Retornar_Lista_Convertida()
    {
        var usuarios = new List<Usuario>
        {
            new() { Id = 1, Nome = "Ana", Email = "ana@email.com" },
            new() { Id = 2, Nome = "Julia", Email = "julia@email.com" }
        };

        usuarioRepositoryMock.Setup(r => r.GetAllUsuariosAsync()).ReturnsAsync(usuarios);

        var result = await usuarioService.GetAllUsuariosAsync();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, u => u.Nome == "Ana");
        Assert.Contains(result, u => u.Nome == "Julia");
    }

    [Fact]
    public async Task GetUsuarioByIdAsync_Deve_Retornar_Usuario_Quando_Existir()
    {
        var usuario = new Usuario { Id = 1, Nome = "Ana", Email = "ana@email.com" };

        usuarioRepositoryMock.Setup(r => r.GetUsuarioByIdAsync(1)).ReturnsAsync(usuario);

        var result = await usuarioService.GetUsuarioByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Ana", result.Nome);
    }

    [Fact]
    public async Task GetUsuarioByIdAsync_Deve_Lancar_Excecao_Quando_Usuario_Nao_Existir()
    {
        usuarioRepositoryMock.Setup(r => r.GetUsuarioByIdAsync(It.IsAny<int>())).ReturnsAsync((Usuario)null!);

        await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() =>
            usuarioService.GetUsuarioByIdAsync(999));
    }
}
