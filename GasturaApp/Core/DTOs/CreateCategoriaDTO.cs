using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class CreateCategoriaDTO
{
    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public int UsuarioId { get; set; }
}
