using GasturaApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.Entities;

public class Gasto
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

    public Usuario Usuario { get; set; }
    public Categoria Categoria { get; set; }
}
