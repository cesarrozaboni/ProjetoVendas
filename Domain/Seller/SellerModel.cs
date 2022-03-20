using Domain.Sales;
using ProjetoVendas.Models.Departament;

namespace Domain.Seller
{
    public class SellerModel
    {
        public int Id                              { get; set; }
        public string Name                         { get; set; }
        public string Email                        { get; set; }
        public DateTime BirthDate                  { get; set; }
        public decimal BaseSalary                   { get; set; }
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
