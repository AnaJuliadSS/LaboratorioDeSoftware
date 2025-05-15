namespace GasturaApp.Infrastructure.Exceptions;

public class EmailJaCadastradoException() : Exception($"O e-mail informado já está cadastrado.") { }
