namespace GasturaApp.Infrastructure.Exceptions;

public class CategoriaJaExisteParaUsuarioException(string descricao, int usuarioid) : Exception($"Categoria {descricao} já existe para usuário {usuarioid}.") { }
