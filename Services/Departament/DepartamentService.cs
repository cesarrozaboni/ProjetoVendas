using Infra.Departament;
using ProjetoVendas.Models.Departament;
using Services.ServiceException;

namespace Services.Departament
{
    public class DepartamentService
    {
        #region "Variable global"
        /// <summary>
        /// Access the DepartamentDal
        /// </summary>
        private readonly DepartamentDal _departamentDal;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="departamentDal">inject of dependency</param>
        public DepartamentService(DepartamentDal departamentDal)
        {
            _departamentDal = departamentDal;
        }
        #endregion

        #region "Count Departament"
        /// <summary>
        /// Count amount of departament
        /// </summary>
        /// <returns>amount of departament</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> CountDepartamentAsync()
        {
            try
            {
                return await Task.FromResult(_departamentDal.CountDepartament());
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Insert Departament"
        /// <summary>
        /// Insert new departament
        /// </summary>
        /// <param name="model">model with data to insert of departament</param>
        /// <returns>id from new departament</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> InsertDepartamentAsync(DepartamentModel model)
        {
            try
            {
                var result = await Task.FromResult(_departamentDal.InsertDepartament(model));

                if (result == 0)
                    throw new IntegrityException("Erro ao Cadastrar novo departamento");
                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Update Departament"
        /// <summary>
        /// Update Departament for id
        /// </summary>
        /// <param name="model">model with data from departament</param>
        /// <returns>1=updated;0=error;</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> UpdateDepartamentAsync(DepartamentModel model)
        {
            try
            {
                return await Task.FromResult(_departamentDal.UpdateDepartament(model));
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Delete Departament"
        /// <summary>
        /// Delete the departament send in parameter
        /// </summary>
        /// <param name="id">Id from departament</param>
        /// <returns><see cref="int"/> rows affected</returns>
        /// <exception cref="IntegrityException"></exception>
        public async Task<int> DeleteDepartamentAsync(int id)
        {
            try
            {
                var result = await Task.FromResult(_departamentDal.DeleteDepartament(id));

                if (result <= 0)
                {
                    throw new IntegrityException("Não foi possivel excluir o departamento");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Get Departament for Id"
        /// <summary>
        /// Return a list with all departaments
        /// </summary>
        /// <param name="id">Id from departament</param>
        /// <returns>A <see cref="DepartamentModel" /> that contains the departament with id informed</returns>
        /// <exception cref="IntegrityException">Excpetion generated in Dal</exception>
        public async Task<DepartamentModel> GetDepartamentForIdAsync(int id)
        {
            try
            {
                var result = await Task.FromResult(_departamentDal.GetDepartamentForId(id));

                if (result == null)
                    throw new IntegrityException("Id Não Encontrado");

                return result;
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion

        #region "Get All Departament"
        /// <summary>
        /// Return a list with all departaments
        /// </summary>
        /// <returns>A <see cref="List{DepartamentModel}" /> that contains the all departament</returns>
        /// <exception cref="IntegrityException">excpetion generated in Dal</exception>
        public async Task<List<DepartamentModel>> GetAllDepartamentAsync()
        {
            try
            {
                return await Task.FromResult(_departamentDal.GetAllDepartament().OrderBy(x => x.Name).ToList());
            }
            catch (Exception ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        #endregion
    }
}
