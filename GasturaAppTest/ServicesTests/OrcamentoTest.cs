using Moq;
using GasturaApp.Application.Services.Implementations;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;

namespace GasturaAppTest.ServiceTests;
public class OrcamentoServiceTest
{
    private readonly Mock<IOrcamentoRepository> orcamentoRepositoryMock;
    private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
    private readonly Mock<ICategoriaRepository> categoriaRepositoryMock;
    private readonly OrcamentoService orcamentoService;

    public OrcamentoServiceTest()
    {
        orcamentoRepositoryMock = new Mock<IOrcamentoRepository>();
        usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        categoriaRepositoryMock = new Mock<ICategoriaRepository>();

        orcamentoService = new OrcamentoService(
            orcamentoRepositoryMock.Object,
            usuarioRepositoryMock.Object,
            categoriaRepositoryMock.Object
        );
    }

    [Fact]
    public async Task ValidarEAdicionarOrcamentoAsync_ComSucesso()
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = 1,
            CategoriaId = 2,
            ValorLimite = 1000m,
            MesReferencia = new DateTime(2025, 6, 1)
        };

        usuarioRepositoryMock.Setup(x => x.UsuarioExisteAsync(dto.UsuarioId))
            .ReturnsAsync(true);

        categoriaRepositoryMock.Setup(x => x.GetCategoriaByIdEUsarioAsync(dto.CategoriaId, dto.UsuarioId))
            .ReturnsAsync(new Categoria { Id = dto.CategoriaId, UsuarioId = dto.UsuarioId });

        orcamentoRepositoryMock.Setup(x => x.AdicionarOrcamentoAsync(It.IsAny<Orcamento>()))
            .ReturnsAsync((Orcamento o) => o);

        // Act
        var result = await orcamentoService.ValidarEAdicionarOrcamentoAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.UsuarioId, result.UsuarioId);
        Assert.Equal(dto.CategoriaId, result.CategoriaId);
        Assert.Equal(dto.ValorLimite, result.ValorLimite);
        Assert.Equal(dto.MesReferencia, result.MesReferencia);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ValidarEAdicionarOrcamentoAsync_ComUsuarioIdInvalido_DeveLancarCampoInvalidoException(int usuarioIdInvalido)
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = usuarioIdInvalido,
            CategoriaId = 1,
            ValorLimite = 100,
            MesReferencia = DateTime.Now
        };

        // Act & Assert
        var ex = await Assert.ThrowsAsync<CampoInvalidoException>(() => orcamentoService.ValidarEAdicionarOrcamentoAsync(dto));
        Assert.Contains("Usuário ID", ex.Message);
    }

    [Fact]
    public async Task ValidarEAdicionarOrcamentoAsync_QuandoUsuarioNaoExiste_DeveLancarEntidadeNaoEncontradaException()
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = 10,
            CategoriaId = 1,
            ValorLimite = 100,
            MesReferencia = DateTime.Now
        };

        usuarioRepositoryMock.Setup(x => x.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(false);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() => orcamentoService.ValidarEAdicionarOrcamentoAsync(dto));
        Assert.Contains("Usuário", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task ValidarEAdicionarOrcamentoAsync_ComCategoriaIdInvalido_DeveLancarCampoInvalidoException(int categoriaIdInvalido)
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = 1,
            CategoriaId = categoriaIdInvalido,
            ValorLimite = 100,
            MesReferencia = DateTime.Now
        };

        usuarioRepositoryMock.Setup(x => x.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(true);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<CampoInvalidoException>(() => orcamentoService.ValidarEAdicionarOrcamentoAsync(dto));
        Assert.Contains("Categoria ID", ex.Message);
    }

    [Fact]
    public async Task ValidarEAdicionarOrcamentoAsync_QuandoCategoriaNaoExiste_DeveLancarEntidadeNaoEncontradaException()
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = 1,
            CategoriaId = 99,
            ValorLimite = 100,
            MesReferencia = DateTime.Now
        };

        usuarioRepositoryMock.Setup(x => x.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(x => x.GetCategoriaByIdEUsarioAsync(dto.CategoriaId, dto.UsuarioId))
            .ReturnsAsync((Categoria?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<EntidadeNaoEncontradaException>(() => orcamentoService.ValidarEAdicionarOrcamentoAsync(dto));
        Assert.Contains("Categoria", ex.Message);
    }

    [Fact]
    public async Task ValidarEAdicionarOrcamentoAsync_ComValorLimiteNegativo_DeveLancarCampoInvalidoException()
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = 1,
            CategoriaId = 1,
            ValorLimite = -100,
            MesReferencia = DateTime.Now
        };

        usuarioRepositoryMock.Setup(x => x.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(x => x.GetCategoriaByIdEUsarioAsync(dto.CategoriaId, dto.UsuarioId))
            .ReturnsAsync(new Categoria());

        // Act & Assert
        var ex = await Assert.ThrowsAsync<CampoInvalidoException>(() => orcamentoService.ValidarEAdicionarOrcamentoAsync(dto));
        Assert.Contains("Valor Limite", ex.Message);
    }

    [Fact]
    public async Task ValidarEAdicionarOrcamentoAsync_ComMesReferenciaDefault_DeveLancarCampoInvalidoException()
    {
        // Arrange
        var dto = new CreateOrcamentoDTO
        {
            UsuarioId = 1,
            CategoriaId = 1,
            ValorLimite = 100,
            MesReferencia = default
        };

        usuarioRepositoryMock.Setup(x => x.UsuarioExisteAsync(dto.UsuarioId)).ReturnsAsync(true);
        categoriaRepositoryMock.Setup(x => x.GetCategoriaByIdEUsarioAsync(dto.CategoriaId, dto.UsuarioId))
            .ReturnsAsync(new Categoria());

        // Act & Assert
        var ex = await Assert.ThrowsAsync<CampoInvalidoException>(() => orcamentoService.ValidarEAdicionarOrcamentoAsync(dto));
        Assert.Contains("Mês de Referência", ex.Message);
    }
}
