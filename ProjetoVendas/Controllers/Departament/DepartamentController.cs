using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ProjetoVendas.Controllers.Departament
{
    public class DepartamentController : Controller
    {
        public IActionResult Index()
        {
            //List<DepartamentModel> list = new();

            //list.Add(new DepartamentModel { Id = 1, Name = "teste" });
            //list.Add(new DepartamentModel { Id = 2, Name = "teste2" });

            return View();
        }
    }
}
