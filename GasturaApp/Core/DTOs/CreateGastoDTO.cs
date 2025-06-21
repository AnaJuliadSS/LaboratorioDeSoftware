using GasturaApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class CreateGastoDTO
{
    [Required(ErrorMessage = "O campo valor é obrigatório.")]
    [Range(0.0, Double.MaxValue, ErrorMessage = "O valor deve ser maior que zero e menor que {1}")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O campo modalidade de pagamento é obrigatória.")]
    public ModalidadePagamento ModalidadePagamento { get; set; }

    [Required(ErrorMessage = "O campo usuário ID é obrigatorio.")]
    public int UsuarioId { get; set; }

    public int? CategoriaId { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public DateTime? DataHora { get; set; }
}
