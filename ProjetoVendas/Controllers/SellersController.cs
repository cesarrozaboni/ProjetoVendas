using Domain.Seller;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Models;
using ProjetoVendas.ViewModels;
using Services.Departament;
using Services.Seller;
using Services.ServiceException;
using System.Diagnostics;

namespace ProjetoVendas.Controllers
{
    public class SellersController : Controller
    {
        #region "Global Variable"
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor class
        /// </summary>
        /// <param name="sellerService">dependency injection</param>
        /// <param name="departamentService">dependency injection</param>
        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
            _sellerService      = sellerService;
            _departamentService = departamentService;
        }
        #endregion

        #region "Index"
        /// <summary>
        /// Index View
        /// </summary>
        /// <returns><see cref="IActionResult"/> View</returns>
        public async Task<IActionResult> Index()
        {
            var result = await _sellerService.GetAllSellerAsync();
            return View(result);
        }
        #endregion

        #region "Create new Seller"
        /// <summary>
        /// View to create new Seller
        /// </summary>
        /// <returns><see cref="IActionResult"/> View</returns>
        public async Task<IActionResult> CreateView()
        {
            try
            {
                var departaments = await _departamentService.GetAllDepartamentAsync();
                var viewModel = new SellerViewModel { Departament = departaments };
                return View(viewModel);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Create new seller in database
        /// </summary>
        /// <param name="SellerModel">Model with values to create a new seller</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SellerModel SellerModel)
        {
            if (!ModelState.IsValid)
            {
                var departaments = await _departamentService.GetAllDepartamentAsync();
                var viewModel = new SellerViewModel { Departament = departaments };
                return View(nameof(CreateView), viewModel);
            }

            try
            {
                await _sellerService.InsertSellerAsync(SellerModel);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
        }
        #endregion

        #region "Delete Seller"
        /// <summary>
        /// Get view to delete a seller
        /// </summary>
        /// <param name="id">id from seller to delete</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                var result = await _sellerService.GetSellerForIdAsync(id);
                return View(result);
            }
            catch(IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete the seller from id
        /// </summary>
        /// <param name="id">id from seller</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.DeleteSellerAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Details View"
        public async Task<IActionResult> DetailsView(int id)
        {
            try
            {
                var result = await _sellerService.GetSellerForIdAsync(id);
                return View(result);
            }
            catch(IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Edit view"
        /// <summary>
        /// View to edit a seller
        /// </summary>
        /// <param name="id">id from seller</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        public async Task<IActionResult> EditView(int id)
        {
            try
            {
                SellerViewModel vm = new SellerViewModel() {
                                                              Departament = await _departamentService.GetAllDepartamentAsync(),
                                                              SellerModel = await _sellerService.GetSellerForIdAsync(id)
                                                           };
                return View(vm);
            }
            catch(IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        /// <summary>
        /// Edit a seller in database
        /// </summary>
        /// <param name="SellerModel">model with values of seller to edit</param>
        /// <param name="id">id to validation</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SellerModel SellerModel, int id)
        {
            try
            {
                if (id != SellerModel.Id)
                    throw new IntegrityException("Operação Invalida");
            
                if (!ModelState.IsValid)
                {
                    SellerViewModel vm = new SellerViewModel() {
                                                                  Departament = await _departamentService.GetAllDepartamentAsync(),
                                                                  SellerModel = await _sellerService.GetSellerForIdAsync(id)
                                                               };
                    return View(vm);
                }

                await _sellerService.UpdateSellerAsync(SellerModel);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Error"
        /// <summary>
        /// view to show error in application
        /// </summary>
        /// <param name="message">message with error</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
        #endregion
    }
}
