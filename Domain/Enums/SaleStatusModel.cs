using System.ComponentModel;

namespace Domain.Enums
{
    public enum SaleStatusModel 
    {
        [Description("Pendente")]
        Pendente,
        [Description("Pago")]
        Pago,
        [Description("Cancelado")]
        Cancelado
    }
}
