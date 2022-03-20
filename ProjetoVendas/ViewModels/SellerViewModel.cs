using Domain.Seller;
using ProjetoVendas.Models.Departament;

namespace ProjetoVendas.ViewModels
{
    public class SellerViewModel
    {
        public SellerModel SellerModel { get; set; }
        public ICollection<DepartamentModel> Departament { get; set; }
    }
}
