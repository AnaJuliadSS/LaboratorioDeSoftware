using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class ListOrcamentoDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int CategoriaId { get; set; }

    [Required]
    public decimal ValorLimite { get; set; }

    [Required]
    public DateTime MesReferencia { get; set; }
}
