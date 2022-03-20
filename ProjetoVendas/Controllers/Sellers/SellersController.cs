using Domain.Seller;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Models;
using ProjetoVendas.ViewModels;
using Services.Departament;
using Services.Exception;
using Services.Seller;
using System.Diagnostics;

namespace ProjetoVendas.Controllers.Sellers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;

        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
            _sellerService = sellerService;
            _departamentService = departamentService;
        }

        public IActionResult Index()
        {
            var result = _sellerService.GetAllSeller();
            return View(result);
        }

        public IActionResult Create()
        {
            var departaments = _departamentService.GetAllDepartament();
            var viewModel = new SellerViewModel() { Departament = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SellerModel SellerModel)
        {
            _sellerService.InsertSeller(SellerModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteView(int id)
        {
            var result = _sellerService.GetSellerForId(id);

            if (result.Id == 0)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.DeleteSeller(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DetailsView(int id)
        {
            var result = _sellerService.GetSellerForId(id);

            if (result.Id == 0)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            var deparmanent = _departamentService.GetDepartamentForId(result.Departament.Id);
            result.Departament = deparmanent;

            return View(result);
        }

        public IActionResult EditView(int id)
        {
            var result = _sellerService.GetSellerForId(id);

            if (result == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            var deparmanent = _departamentService.GetAllDepartament();
            SellerViewModel vm = new SellerViewModel();
            vm.Departament = deparmanent;
            vm.SellerModel = result;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SellerModel SellerModel)
        {
            //if(id != SellerModel.Id)
            //{
            //    return BadRequest();
            //}

            try
            {
                _sellerService.UpdateSeller(SellerModel);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
