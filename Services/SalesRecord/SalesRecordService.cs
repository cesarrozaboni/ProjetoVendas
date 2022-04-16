using Domain.Sales;
using Infra.Departament;
using Infra.SalesRecord;
using Infra.Seller;
using Services.Departament;
using Services.Seller;
using Services.ServiceException;

namespace Services.SalesRecord
{
    public class SalesRecordService
    {
        #region "Variable global"
        /// <summary>
        /// Access the SalesDao
        /// </summary>
        private readonly SalesRecordDal _salesRecordDal;
        public SellerService _sellerService;
        public readonly DepartamentService _departamentService;
        #endregion

        #region "Propriedades"
        public SellerService SellerService
        {
            get { return _sellerService == null ? new SellerService(new SellerDal()) : _sellerService; }
        }

        public DepartamentService DepartamentService 
        {
            get { return _departamentService ?? new DepartamentService(new DepartamentDal()); }
        }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="salesRecordDal">inject of dependency</param>
        public SalesRecordService(SalesRecordDal salesRecordDal)
        {
            _salesRecordDal = salesRecordDal;
        }
        #endregion

        #region "Count Sales Async"
        /// <summary>
        /// Count amount Sales registred
        /// </summary>
        /// <returns><see cref="Task{int}"/>amount sales registred</returns>
        public async Task<int> CountSalesAsync()
        {
            try
            {
                return await Task.FromResult(_salesRecordDal.CountSales());
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Insert Sales"
        /// <summary>
        /// Insert new Sales
        /// </summary>
        /// <param name="model">model with data to insert of Sales</param>
        /// <returns>id from new Sales</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> InsertSalesAsync(SalesRecordModel model)
        {
            try
            {
                var result = await Task.FromResult(_salesRecordDal.InsertSales(model));

                if(result ==0)
                    throw new IntegrityException("Erro ao registrar nova venda");

                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Update Sales"
        /// <summary>
        /// Update Sales for id
        /// </summary>
        /// <param name="model">model with data from Sales</param>
        /// <returns><see cref="int"/> rows affected</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> UpdateSalesAsync(SalesRecordModel model)
        {
            try
            {
                var result = await Task.FromResult(_salesRecordDal.UpdateSales(model));

                if(result ==0)
                    throw new IntegrityException("Erro ao atualizar venda");

                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Delete Sales"
        /// <summary>
        /// Delete the Sales send in parameter
        /// </summary>
        /// <param name="id">Id from Sales</param>
        /// <returns><see cref="int"/> rows affected </returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> DeleteSalesAsync(int id)
        {
            try
            {
                return await Task.FromResult(_salesRecordDal.DeleteSales(id));
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Get Sales for Id"
        /// <summary>
        /// Return a list with Sales using id
        /// </summary>
        /// <param name="id">Id from Sales</param>
        /// <returns>A <see cref="SalesRecordModel" /> that contains the Sales with id informed</returns>
        /// <exception cref="IntegrityException">Excpetion generated in Dal</exception>
        public async Task<SalesRecordModel> GetSalesForIdAsync(int id)
        {
            try
            {
                _sellerService = new SellerService(new SellerDal());
                var result = await Task.FromResult(_salesRecordDal.GetSalesForId(id));

                if(result == null)
                    throw new IntegrityException("Não foi possivel encontrar a venda informada!");

                result.Seller = await _sellerService.GetSellerForIdAsync(result.Seller.Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Get All Sales Async"
        public async Task<List<SalesRecordModel>> GetAllSalesAsync()
        {
            var salesResult = await Task.FromResult(_salesRecordDal.GetAllSales());

            foreach (var item in salesResult)
            {
                item.Seller                    = await SellerService.GetSellerForIdAsync(item.Seller.Id);
                item.Seller.Departament        = await DepartamentService.GetDepartamentForIdAsync(item.Seller.Departament.Id);
                item.Seller.Departament.Seller = SellerService.GetAllSellerAsync().Result.Where(x => x.Departament.Id == item.Seller.Departament.Id).ToList();
            }

            return salesResult;
        }
        #endregion

        #region "Find by date async"
        /// <summary>
        /// Find sales by dates async
        /// </summary>
        /// <param name="minDate">Min date to search</param>
        /// <param name="maxDate">Max date to search</param>
        /// <returns><see cref="List{SalesRecordModel}"/>object with values</returns>
        public async Task<List<SalesRecordModel>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            try
            {
                var result = await this.GetAllSalesAsync();
                if (minDate.HasValue)
                    result = result.Where(x => x.Date >= minDate.Value).ToList();

                if (maxDate.HasValue)
                    result = result.Where(x => x.Date <= maxDate.Value).ToList();

                return result;
            }
            catch(Exception ex)
            {
                throw new IntegrityException("Erro para realizar pesquisa: " + ex.Message);
            }

           
        }
        #endregion

        #region "Find by group async"
        public async Task<List<SalesGroupModel>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = await FindByDateAsync(minDate, maxDate);
            result = result.OrderByDescending(x => x.Seller.Departament.Id).OrderByDescending(x => x.Seller.Id).ToList();
            
            string departament               = string.Empty;
            string lastDepartament           = string.Empty;
            List<SalesGroupModel> salesGroup = new List<SalesGroupModel>();
            List<SalesGroup> sales           = new List<SalesGroup>();

            for (int index = 0; index < result.Count; index++)
            {
                departament = result[index].Seller.Departament.Name ?? string.Empty;

                if (ChangedDepartament(departament, lastDepartament))
                {
                    salesGroup.Add(PopulateGroup(departament, result, sales));
                    sales = new List<SalesGroup>();
                }

                sales.Add(new SalesGroup {
                                            Amount = result[index].Amount,
                                            Data   = result[index].Date,
                                            Seller = result[index].Seller.Name,
                                            Status = result[index].Status
                                          });

                
                if(index == result.Count - 1)
                    salesGroup.Add(PopulateGroup(departament, result, sales));
                
                lastDepartament = departament;
            }

            return salesGroup;
        }

        /// <summary>
        /// verify if change departament
        /// </summary>
        /// <param name="dep">Departament current</param>
        /// <param name="lastDep">Last Departament</param>
        /// <returns><see cref="bool"/>true=yes;false=no;</returns>
        public bool ChangedDepartament(string dep, string lastDep)
        {
            return !string.IsNullOrWhiteSpace(lastDep) && dep != lastDep;
        }

        /// <summary>
        /// make new SalesGroupModel
        /// </summary>
        /// <param name="departament"></param>
        /// <param name="result"></param>
        /// <param name="sales"></param>
        /// <returns></returns>
        public SalesGroupModel PopulateGroup(string departament, List<SalesRecordModel> result, List<SalesGroup> sales)
        {
            return new SalesGroupModel {
                                          NameDepartament  = departament,
                                          TotalVendas      = result.Where(x => x.Seller.Departament.Name == departament).Sum(x => x.Amount),
                                          SalesGroupModels = sales
                                       };
        }
        #endregion
    }
}
