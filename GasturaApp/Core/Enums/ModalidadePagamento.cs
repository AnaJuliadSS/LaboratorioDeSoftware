using System.ComponentModel;

namespace GasturaApp.Core.Enums;

public enum ModalidadePagamento
{
    [Description("Crédito")]
    Credito = 1,
    [Description("Débito")]
    Debito,
    [Description("Dinheiro")]
    Dinheiro,
    [Description("Pix")]
    Pix
}
