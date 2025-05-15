namespace GasturaApp.Infrastructure.Exceptions;

public class CampoInvalidoException(string nomeCampo) : Exception($"O campo '{nomeCampo}' informado é inválido.") { }
