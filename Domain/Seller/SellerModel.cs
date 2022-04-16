using Domain.Sales;
using ProjetoVendas.Models.Departament;
using System.ComponentModel.DataAnnotations;

namespace Domain.Seller
{
    public class SellerModel
    {
        public int Id                              { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} tamanho deve estar entre {2} e {1}" )]
        public string Name                         { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} Required")]
        [EmailAddress(ErrorMessage = "Informe um email valido")]
        public string Email                        { get; set; }

        [Display(Name="Data de Nascimento")]
        [Required(ErrorMessage = "{0} Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate                  { get; set; }

        [Display(Name="Salario")]
        [Required(ErrorMessage = "{0} Required")]
        [Range(100, 50000, ErrorMessage = "{0} precisa estar entre {1} e {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal BaseSalary                  { get; set; }
        [Display(Name = "Departamento")]
        public DepartamentModel Departament        { get; set; }
        public ICollection<SalesRecordModel> Sales { get; set; } = new List<SalesRecordModel>();

        public SellerModel()
        {
        }

        public SellerModel(int id, string name, string email, DateTime birthDate, decimal baseSalary, DepartamentModel departament)
        {
            Id          = id;
            Name        = name;
            Email       = email;
            BirthDate   = birthDate;
            BaseSalary  = baseSalary;
            Departament = departament;
        }

        public void AddSales(SalesRecordModel sales)
        {
            Sales.Add(sales);
        }

        public void RemoveSales(SalesRecordModel sales)
        {
            Sales.Remove(sales);
        }

        public decimal TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sales => sales.Date >= initial && sales.Date <= final).Sum(sales => sales.Amount);
        }
    }
}
