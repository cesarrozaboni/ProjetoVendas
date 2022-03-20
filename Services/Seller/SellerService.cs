using Domain.Seller;
using Infra.Seller;

namespace Services.Seller
{
    public class SellerService
    {
        public int CountSeller()
        {
            SellerDao seller = new SellerDao();

            return seller.CountSeller();
        }

        public int InsertSales(SellerModel model)
        {
            SellerDao seller = new SellerDao();

            return seller.InsertSeller(model);
        }

        public int UpdateSales(SellerModel model)
        {
            SellerDao seller = new SellerDao();

            return seller.UpdateSeller(model);
        }

        public int DeleteSales(int id)
        {
            SellerDao seller = new SellerDao();

            return seller.DeleteSeller(id);
        }

        public SellerModel GetSellerForId(int id)
        {
            SellerDao seller = new SellerDao();

            return seller.GetSellerForId(id);
        }

        public List<SellerModel> GetAllSeller()
        {
            SellerDao seller = new SellerDao();

            return seller.GetAllSeller();
        }
    }
}
