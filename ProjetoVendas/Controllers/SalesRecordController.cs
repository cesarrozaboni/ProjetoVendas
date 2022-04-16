using Domain.Sales;
using Infra.Seller;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas.Models;
using Services.SalesRecord;
using Services.Seller;
using Services.ServiceException;
using System.Diagnostics;

namespace ProjetoVendas.Controllers
{
    public class SalesRecordController : Controller
    {
        #region "Global Variable"
        private readonly SalesRecordService _salesRecordService;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor class
        /// </summary>
        /// <param name="salesRecordService">dependency injection</param>
        public SalesRecordController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }
        #endregion

        #region "Index"
        /// <summary>
        /// get view Index
        /// </summary>
        /// <returns><see cref="IActionResult"/> View</returns>
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region "Create Sales"
        /// <summary>
        /// View to create a new sale
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateView()
        {
            ViewData["ListSales"] = new SalesStatus().GetEnumSales();
            ViewData["ListSeller"] = await new SellerService(new SellerDal()).GetAllSellerAsync();
            return View();
        }

        /// <summary>
        /// create a new sale in database
        /// </summary>
        /// <param name="model">model with values</param>
        /// <returns></returns>
        public async Task<IActionResult> Create(SalesRecordModel model)
        {
            try
            {
                await _salesRecordService.InsertSalesAsync(model);
                return RedirectToAction(nameof(CreateView));
            }
            catch(IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Edit sale"
        /// <summary>
        /// View to edit a sale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditView(int id)
        {
            try 
            {
                ViewData["ListSales"] = new SalesStatus().GetEnumSales();
                ViewData["ListSeller"] = await new SellerService(new SellerDal()).GetAllSellerAsync();

                var result = await _salesRecordService.GetSalesForIdAsync(id);
                return View(result);
            }
            catch(IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        /// <summary>
        /// edit a sale
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(SalesRecordModel model)
        {
            try
            {
                await _salesRecordService.UpdateSalesAsync(model);
                return RedirectToAction(nameof(SimpleSearch));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message});
            }
        }
        #endregion

        #region "Cancel Sale"
        /// <summary>
        /// View to cancel sale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                var result = await _salesRecordService.GetSalesForIdAsync(id);
                return View(result);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        #endregion

        #region "Simple search"
        /// <summary>
        /// Simple search by date
        /// </summary>
        /// <param name="minDate">min date to search</param>
        /// <param name="maxDate">max date to search</param>
        /// <returns><see cref="IActionResult"/> View</returns>
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) 
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            
            if (!maxDate.HasValue)
                maxDate = DateTime.Now;
            
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            
            try
            { 
                var result =  await _salesRecordService.FindByDateAsync(minDate, maxDate);
                return View(result);
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new {message = ex.Message});
            }
        }
        #endregion

        #region "Group search"
        /// <summary>
        /// Search a group sales
        /// </summary>
        /// <param name="minDate">min date to search</param>
        /// <param name="maxDate">max date to search</param>
        /// <returns></returns>
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
                minDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!maxDate.HasValue)
                maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            try
            {
                var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
                return View(result);
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
