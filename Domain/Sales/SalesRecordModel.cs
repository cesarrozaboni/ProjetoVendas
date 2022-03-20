using Domain.Enum;
using Domain.Seller;

namespace Domain.Sales
{
    public class SalesRecordModel
    {
        public int Id                 { get; set; }
        public DateTime Date          { get; set; }
        public decimal Amount          { get; set; }
        public SaleStatusModel Status { get; set; }
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
