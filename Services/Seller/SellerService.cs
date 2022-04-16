using Domain.Seller;
using Infra.Departament;
using Infra.Seller;
using Services.Departament;
using Services.ServiceException;

namespace Services.Seller
{
    public class SellerService
    {
        #region "Variable global"
        /// <summary>
        /// Access the SellerDao
        /// </summary>
        private readonly SellerDal _sellerDao;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sellerDao">inject of dependency</param>
        public SellerService(SellerDal sellerDao)
        {
            _sellerDao = sellerDao;
        }
        #endregion

        #region "Count Seller"
        /// <summary>
        /// Count amount of seller
        /// </summary>
        /// <returns><see cref="int"/> amount of seller</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> CountSellerAsync()
        {
            try
            {
                return await Task.FromResult(_sellerDao.CountSeller());
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Insert Seller"
        /// <summary>
        /// Insert new Seller
        /// </summary>
        /// <param name="model">model with data to insert of Seller</param>
        /// <returns>id from new Seller</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> InsertSellerAsync(SellerModel model)
        {
            try
            {
                var result = await Task.FromResult(_sellerDao.InsertSeller(model));
                
                if (result == 0)
                    throw new IntegrityException("Erro ao para cadastrar vendedor");
                
                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Update Seller"
        /// <summary>
        /// Update Seller for id
        /// </summary>
        /// <param name="model">model with data from Seller</param>
        /// <returns><see cref="int"/> rows affected</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> UpdateSellerAsync(SellerModel model)
        {
            try
            {
                var result = await Task.FromResult(_sellerDao.UpdateSeller(model));

                if (result == 0)
                    throw new IntegrityException("Não foi possivel atualizar os dados do vendedor");

                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Delete Seller"
        /// <summary>
        /// Delete the Seller send in parameter
        /// </summary>
        /// <param name="id">Id from Seller</param>
        /// <returns><seealso cref="int"/> rows affected </returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> DeleteSellerAsync(int id)
        {
            try
            {
                return await Task.FromResult(_sellerDao.DeleteSeller(id));
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Get Seller for Id"
        /// <summary>
        /// Return a list with Seller using id
        /// </summary>
        /// <param name="id">Id from Seller</param>
        /// <returns>A <see cref="SellerModel" /> that contains the Seller with id informed</returns>
        /// <exception cref="IntegrityException">Excpetion generated in Dal</exception>
        public async Task<SellerModel> GetSellerForIdAsync(int id)
        {
            try
            {
                var result = await Task.FromResult(_sellerDao.GetSellerForId(id));
                
                if (result == null)
                    throw new IntegrityException("Vendedor não encontrado");

                result.Departament = await new DepartamentService(new DepartamentDal()).GetDepartamentForIdAsync(result.Departament.Id);

                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Get All Seller"
        /// <summary>
        /// Return a list with all Seller
        /// </summary>
        /// <returns>A <see cref="List{SellerModel}" /> that contains the all Seller</returns>
        /// <exception cref="IntegrityException">excpetion generated in Dal</exception>
        public async Task<List<SellerModel>> GetAllSellerAsync()
        {
            try
            {
                return await Task.FromResult(_sellerDao.GetAllSeller());
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion
    }
}
