using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.Entities;

public class Orcamento
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int CategoriaId { get; set; }

    [Required]
    public decimal ValorLimite { get; set; }

    [Required]
    public DateTime MesReferencia { get; set; }

    public Usuario Usuario { get; set; }
    public Categoria Categoria { get; set; }

}
