using GasturaApp.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class CreateOrcamentoDTO
{
    [Required(ErrorMessage = "O usuário ID é obrigatório.")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "A categoria ID é obrigatória.")]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O valor limite é obrigatório.")]
    [Range(0, Double.MaxValue, ErrorMessage = "O valor limite deve ser no mínimo 0.")]
    public decimal ValorLimite { get; set; }

    [Required]
    public DateTime MesReferencia { get; set; }
}
