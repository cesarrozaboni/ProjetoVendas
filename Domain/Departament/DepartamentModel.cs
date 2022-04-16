using Domain.Seller;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoVendas.Models.Departament
{
    public class DepartamentModel
    {
        #region "Constructor"
        public DepartamentModel()
        {
        }

        public DepartamentModel(int id, string name)
        {
            Id   = id;
            Name = name;
        }
        #endregion


        [DisplayName("Id")]
        public int Id       { get; set; }
        
        [Required(ErrorMessage ="{0} é obrigatorio")]
        [DisplayName("Nome")]
        public string? Name { get; set; }
        public ICollection<SellerModel> Seller { get; set; } = new List<SellerModel>();

    }
}
