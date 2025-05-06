namespace GasturaApp.Infrastructure.Exceptions
{
    public class CampoInvalidoException : Exception
    {
        public CampoInvalidoException(string nomeCampo)
           : base($"O campo '{nomeCampo}' informado é inválido.")
        {
        }
    }
}
