namespace GasturaApp.Infrastructure.Exceptions;

public class CampoObrigatorioException : Exception
{
    public CampoObrigatorioException(string nomeCampo)
            : base($"O campo '{nomeCampo}' é obrigatório e não foi informado.")
    {
    }
}
