using GasturaApp.Core.Entities;
using GasturaApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class ListGastosDTO
{
    [Key]
    public int Id { get; set; }

    [Required]
    public decimal Valor { get; set; }

    public DateTime? DataHora { get; set; }

    public string Descricao { get; set; } = string.Empty;

    [Required]
    public ModalidadePagamento ModalidadePagamento { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int CategoriaId { get; set; }

    [Required]
    public Categoria Categoria { get; set; }
}
