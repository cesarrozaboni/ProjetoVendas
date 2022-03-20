using Domain.Enum;
using Domain.Sales;
using Domain.Seller;
using ProjetoVendas.Models.Departament;
using Services.Departament;
using Services.SalesRecord;
using Services.Seller;

namespace ProjetoVendas.Data
{
    public class SeedingData
    {
        public void Seed()
        {
            var departament = new DepartamentService();
            var sales = new SalesRecordService();
            var seller = new SellerService();

            if (departament.CountDepartament() > 0 || sales.CountSales() > 0 || seller.CountSeller() > 0 )
                return;

            var dep1 = departament.InsertDepartament(new DepartamentModel { Name = "Eletronics" });
            var dep2 = departament.InsertDepartament(new DepartamentModel { Name = "Computers" });
            var dep3 = departament.InsertDepartament(new DepartamentModel { Name = "Fashion" });
            var dep4 = departament.InsertDepartament(new DepartamentModel { Name = "Books" });

            var sel1 = seller.InsertSeller(new SellerModel { Name = "Joao", Email = "joao@email", BaseSalary = 1800.00M, BirthDate = new DateTime(1998, 4, 21), Departament = new DepartamentModel { Id = dep1 } });
            var sel2 = seller.InsertSeller(new SellerModel { Name = "maria", Email = "maria@email", BaseSalary = 800.00M, BirthDate = new DateTime(1874, 2, 11), Departament = new DepartamentModel { Id = dep1 } });
            var sel3 = seller.InsertSeller(new SellerModel { Name = "luis", Email = "luis@email", BaseSalary = 5800.00M, BirthDate = new DateTime(1964, 11, 01), Departament = new DepartamentModel { Id = dep2 } });
            var sel4 = seller.InsertSeller(new SellerModel { Name = "jose", Email = "jose@email", BaseSalary = 1120.00M, BirthDate = new DateTime(2001, 2, 15), Departament = new DepartamentModel { Id = dep2 } });
            var sel5 = seller.InsertSeller(new SellerModel { Name = "leticia", Email = "leticia@email", BaseSalary = 7800.00M, BirthDate = new DateTime(1997, 6, 22), Departament = new DepartamentModel { Id = dep3 } });
            var sel6 = seller.InsertSeller(new SellerModel { Name = "anastacia", Email = "anastacia@email", BaseSalary = 10800.50M, BirthDate = new DateTime(1974, 9, 30), Departament = new DepartamentModel { Id = dep4 } });

            sales.InsertSales(new SalesRecordModel { Id = 1, Date = new DateTime(2022, 01, 12), Amount = 8000.20M, Seller = new SellerModel { Id =sel1 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 2, Date = new DateTime(2022, 01, 12), Amount = 8000.20M, Seller = new SellerModel { Id =sel1 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 3, Date = new DateTime(2022, 01, 12), Amount = 8900.20M, Seller = new SellerModel { Id =sel1 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 4, Date = new DateTime(2022, 01, 12), Amount = 8900.20M, Seller = new SellerModel { Id =sel1 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 5, Date = new DateTime(2022, 01, 12), Amount = 8900.20M, Seller = new SellerModel { Id =sel1 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 6, Date = new DateTime(2022, 01, 12), Amount = 8900.20M, Seller = new SellerModel { Id =sel1 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 7, Date = new DateTime(2022, 01, 12), Amount = 8900.20M, Seller = new SellerModel { Id = sel1 }, Status = SaleStatusModel.Pending });
            sales.InsertSales(new SalesRecordModel { Id = 8, Date = new DateTime(2022, 01, 12), Amount = 1900.20M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 9, Date = new DateTime(2022, 01, 12), Amount = 1900.20M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 10, Date = new DateTime(2022, 01, 11), Amount = 1900M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 11, Date = new DateTime(2022, 02, 11), Amount = 1000M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Pending });
            sales.InsertSales(new SalesRecordModel { Id = 12, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 13, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 14, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Pending });
            sales.InsertSales(new SalesRecordModel { Id = 15, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel3 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 16, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel3 }, Status = SaleStatusModel.Pending });
            sales.InsertSales(new SalesRecordModel { Id = 17, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel3 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 18, Date = new DateTime(2022, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel3 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 19, Date = new DateTime(2021, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel3 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 20, Date = new DateTime(2021, 02, 11), Amount = 1080M, Seller = new SellerModel { Id = sel3 }, Status = SaleStatusModel.Pending });
            sales.InsertSales(new SalesRecordModel { Id = 21, Date = new DateTime(2021, 02, 19), Amount = 1080M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 22, Date = new DateTime(2021, 01, 19), Amount = 1080M, Seller = new SellerModel { Id = sel5 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 23, Date = new DateTime(2021, 11, 19), Amount = 1000M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Canceled });
            sales.InsertSales(new SalesRecordModel { Id = 24, Date = new DateTime(2021, 11, 19), Amount = 2000M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 25, Date = new DateTime(2021, 11, 19), Amount = 2000M, Seller = new SellerModel { Id = sel6 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 26, Date = new DateTime(2021, 11, 19), Amount = 2000M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 27, Date = new DateTime(2021, 11, 19), Amount = 2000M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Canceled });
            sales.InsertSales(new SalesRecordModel { Id = 28, Date = new DateTime(2021, 11, 19), Amount = 2000M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 29, Date = new DateTime(2021, 11, 10), Amount = 2000M, Seller = new SellerModel { Id = sel4 }, Status = SaleStatusModel.Canceled });
            sales.InsertSales(new SalesRecordModel { Id = 30, Date = new DateTime(2021, 11, 10), Amount = 2000M, Seller = new SellerModel { Id = sel2 }, Status = SaleStatusModel.Billed });
            sales.InsertSales(new SalesRecordModel { Id = 31, Date = new DateTime(2021, 11, 10), Amount = 870.00M, Seller = new SellerModel { Id = sel1 }, Status = SaleStatusModel.Billed });
        }
    }
}
