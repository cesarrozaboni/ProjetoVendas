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

        public int InsertSeller(SellerModel model)
        {
            SellerDao seller = new SellerDao();

            return seller.InsertSeller(model);
        }

        public int UpdateSeller(SellerModel model)
        {
            SellerDao seller = new SellerDao();

            return seller.UpdateSeller(model);
        }

        public int DeleteSeller(int id)
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
