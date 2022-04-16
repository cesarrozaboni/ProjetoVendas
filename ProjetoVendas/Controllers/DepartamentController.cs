using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Models;
using ProjetoVendas.Models.Departament;
using Services.Departament;
using Services.ServiceException;
using System.Diagnostics;

namespace ProjetoVendas.Controllers
{
    public class DepartamentController : Controller
    {
        #region "Global variable"
        /// <summary>
        /// Dependency Injection
        /// </summary>
        private readonly DepartamentService _departamentService;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="departamentService">Injection of dependency</param>
        public DepartamentController(DepartamentService departamentService)
        {
            _departamentService = departamentService;
        }
        #endregion

        #region "Index"
        /// <summary>
        /// Return view index
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _departamentService.GetAllDepartamentAsync();
                return View(result);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = "Erro ao consultar departamentos" });
            }
        }
        #endregion

        #region "Create new departament"
        /// <summary>
        /// View to create a new departament
        /// </summary>
        /// <returns><see cref="ActionResult{ViewResult}"/></returns>
        public IActionResult CreateView()
        {
            return View();
        }

        /// <summary>
        /// Create new Departament
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(DepartamentModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(EditView));

            try
            {
                await _departamentService.InsertDepartamentAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex });
            }
        }
        #endregion

        #region "Edit View"
        /// <summary>
        /// View to edit departament
        /// </summary>
        /// <param name="id">Id from departament to edit</param>
        /// <returns><see cref="ActionResult"/></returns>
        public async Task<IActionResult> EditView(int id)
        {
            try
            {
                var result = await _departamentService.GetDepartamentForIdAsync(id);
                return View(result);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex });
            }
        }

        /// <summary>
        /// Edit the departament for id
        /// </summary>
        /// <param name="model">Model with values to edit the departament</param>
        /// <returns><see cref="int"/> Rows affected</returns>
        public async Task<IActionResult> Edit(DepartamentModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(EditView));

            try
            {
                await _departamentService.UpdateDepartamentAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Details View"
        /// <summary>
        /// View to show details of departament
        /// </summary>
        /// <param name="id">id to view details</param>
        /// <returns><see cref="IActionResult"/> Details view</returns>
        public async Task<IActionResult> DetailsView(int id)
        {
            try
            {
                var result = await _departamentService.GetDepartamentForIdAsync(id);
                return View(result);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Delete View"
        /// <summary>
        /// view to delete a departament
        /// </summary>
        /// <param name="id">id from departament</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                var result = await _departamentService.GetDepartamentForIdAsync(id);
                return View(result);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a departament
        /// </summary>
        /// <param name="id">Id from departament</param>
        /// <returns>Delete the departament with id from parameter</returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departamentService.DeleteDepartamentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
        }
        #endregion

        #region "Error View"
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
