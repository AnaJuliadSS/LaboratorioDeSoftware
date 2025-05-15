namespace GasturaApp.Infrastructure.Exceptions;

public class SenhaNaoDeveConterSequenciasException() : Exception($"A senha não pode conter sequências numéricas ou alfabéticas.") { }

