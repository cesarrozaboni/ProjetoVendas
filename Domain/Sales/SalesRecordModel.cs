using Domain.Enums;
using Domain.Seller;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Sales
{
    public class SalesRecordModel
    {
        [DisplayName("Id")]
        public int Id                 { get; set; }
        [DisplayName("Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date          { get; set; }
        [DisplayName("Valor")]
        public decimal Amount          { get; set; }
        [DisplayName("Status")]
        public SaleStatusModel Status { get; set; }
        [DisplayName("Vendedor")]
        public SellerModel Seller     { get; set; }
        

        public SalesRecordModel()
        {
        }

        public SalesRecordModel(int id, DateTime date, decimal amount, SaleStatusModel status, SellerModel seller)
        {
            Id     = id;
            Date   = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
