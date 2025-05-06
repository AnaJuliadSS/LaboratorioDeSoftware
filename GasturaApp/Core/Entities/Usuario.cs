using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.Entities;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public DateTime DataCadastro { get; set; }
}
