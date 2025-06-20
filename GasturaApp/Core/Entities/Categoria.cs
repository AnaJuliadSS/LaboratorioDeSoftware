using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.Entities;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public int UsuarioId { get; set; }

    public string Cor { get; set; }

    public Usuario Usuario { get; set; }
}
