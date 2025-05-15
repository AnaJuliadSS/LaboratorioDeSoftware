namespace GasturaApp.Infrastructure.Exceptions;

public class EntidadeNaoEncontradaException(string entidade) : Exception($"Entidade {entidade} não encontrada."){ }
