using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class CreateCategoriaDTO
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
    public int UsuarioId { get; set; }

    public string Cor { get; set; } = string.Empty;
}
