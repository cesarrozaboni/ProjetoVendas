using Domain.Seller;

namespace ProjetoVendas.Models.Departament
{
    public class DepartamentModel
    {
        public int Id      { get; set; }
        public string Name { get; set; }
        public ICollection<SellerModel> Seller { get; set; } = new List<SellerModel>();

        public DepartamentModel()
        {

        }

        public DepartamentModel(int id, string name)
        {
            Id   = id;
            Name = name;
        }

        public void AddSeller(SellerModel seller)
        {
            Seller.Add(seller);
        }

        public decimal TotalSales(DateTime initial, DateTime final)
        {
            return Seller.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
