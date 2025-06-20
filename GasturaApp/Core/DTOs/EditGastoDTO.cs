using GasturaApp.Core.Entities;
using GasturaApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class EditGastoDTO
{

    public decimal? Valor { get; set; }

    public DateTime? DataHora { get; set; }

    public string? Descricao { get; set; } = string.Empty;

    public ModalidadePagamento? ModalidadePagamento { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    public int? CategoriaId { get; set; }
}
