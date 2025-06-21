using System.ComponentModel.DataAnnotations;

namespace GasturaApp.Core.DTOs;

public class EditOrcamentoDTO
{
    public int UsuarioId { get; set; }

    public int CategoriaId { get; set; }

    public decimal ValorLimite { get; set; }

    public DateTime MesReferencia { get; set; }
}
