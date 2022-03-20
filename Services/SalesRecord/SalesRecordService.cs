using Domain.Sales;
using Infra.SalesRecord;

namespace Services.SalesRecord
{
    public class SalesRecordService
    {
        public int CountSales()
        {
            SalesRecordDal sales = new SalesRecordDal();

            return sales.CountSales();
        }

        public int InsertSales(SalesRecordModel model)
        {
            SalesRecordDal sales = new SalesRecordDal();

            return sales.InsertSales(model);
        }

        public int UpdateSales(SalesRecordModel model)
        {
            SalesRecordDal sales = new SalesRecordDal();

            return sales.UpdateSales(model);
        }

        public int DeleteSales(int id)
        {
            SalesRecordDal sales = new SalesRecordDal();

            return sales.DeleteSales(id);
        }

        public SalesRecordModel GetSellerForId(int id)
        {
            SalesRecordDal sales = new SalesRecordDal();

            return sales.GetSalesForId(id);
        }

        public List<SalesRecordModel> GetAllSeller()
        {
            SalesRecordDal sales = new SalesRecordDal();

            return sales.GetAllSales();
        }
    }
}
