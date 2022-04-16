using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Sales
{
    public class SalesGroupModel
    {
        public string NameDepartament { get; set; }
        public decimal TotalVendas { get; set; }

        public List<SalesGroup> SalesGroupModels { get; set; } = new List<SalesGroup>();
    }

    public class SalesGroup
    {
        [DisplayName("Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        [DisplayName("Valor")]
        public decimal Amount { get; set; }
        [DisplayName("Vendedor")]
        public string Seller { get; set; }
        [DisplayName("Status")]
        public SaleStatusModel Status { get; set; }

    }
}
