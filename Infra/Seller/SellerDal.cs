using Domain.Seller;
using ProjetoVendas.Models.Departament;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Seller
{
    public class SellerDal: InfraBase
    {
        #region "Procedures"
        public const string SP_SEL_COUNT    = "SP_SEL_COUNT";
        public const string SP_SEL_INSERT   = "SP_SEL_INSERT";
        public const string SP_SEL_UPDATE   = "SP_SEL_UPDATE";
        public const string SP_SEL_DELETE   = "SP_SEL_DELETE";
        public const string SP_SEL_GETFORID = "SP_SEL_GETFORID";
        public const string SP_SEL_GETALL   = "SP_SEL_GETALL";
        #endregion

        #region "Parameters"
        public const string PARAM_ID             = "@P_ID";
        public const string PARAM_NAME           = "@P_NAME";
        public const string PARAM_EMAIL          = "@P_EMAIL";
        public const string PARAM_BIRTHDATE      = "@P_BIRTHDATE";
        public const string PARAM_SALARY_BASE    = "@P_SALARY_BASE";
        public const string PARAM_DEPARTAMENT_ID = "@P_DEPARTAMENT_ID";
        #endregion

        #region "Reader"
        public const string READER_ID             = "SELLER_ID";
        public const string READER_NAME           = "SELLER_NAME";
        public const string READER_EMAIL          = "SELLER_EMAIL";
        public const string READER_BITHDATE       = "SELLER_BIRTHDATE";
        public const string READER_SALARY_BASE    = "SELLER_SALARY_BASE";
        public const string READER_DEPARTAMENT_ID = "SELLER_ID_DEPARTAMENT";
        #endregion

        #region "Count Seller"
        /// <summary>
        /// Count amount Seller in database
        /// </summary>
        /// <returns><see cref="int"/> With number of seller</returns>
        public int CountSeller()
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SEL_COUNT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Insert Seller"
        /// <summary>
        /// Insert new seller in database
        /// </summary>
        /// <param name="model">Model with values to insert seller</param>
        /// <returns><see cref="int"/> 1=ok; 0=error;</returns>
        public int InsertSeller(SellerModel model)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SEL_INSERT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_NAME, model.Name));
                cmd.Parameters.Add(new SqlParameter(PARAM_EMAIL, model.Email));
                cmd.Parameters.Add(new SqlParameter(PARAM_BIRTHDATE, model.BirthDate));
                cmd.Parameters.Add(new SqlParameter(PARAM_SALARY_BASE, model.BaseSalary));
                cmd.Parameters.Add(new SqlParameter(PARAM_DEPARTAMENT_ID, model.Departament.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Update Seller"
        /// <summary>
        /// Update Seller in database using id
        /// </summary>
        /// <param name="model">Model with values to update seller</param>
        /// <returns><see cref="int"/> Rows affected</returns>
        public int UpdateSeller(SellerModel model)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SEL_UPDATE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, model.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_NAME, model.Name));
                cmd.Parameters.Add(new SqlParameter(PARAM_EMAIL, model.Email));
                cmd.Parameters.Add(new SqlParameter(PARAM_BIRTHDATE, model.BirthDate));
                cmd.Parameters.Add(new SqlParameter(PARAM_SALARY_BASE, model.BaseSalary));
                cmd.Parameters.Add(new SqlParameter(PARAM_DEPARTAMENT_ID, model.Departament.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Delete Seller"
        /// <summary>
        /// Delete seller in database
        /// </summary>
        /// <param name="id">Id to delete seller</param>
        /// <returns><see cref="int"/> rows affected</returns>
        public int DeleteSeller(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SEL_DELETE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Get seller For Id
        /// <summary>
        /// Get Seller for id in database
        /// </summary>
        /// <param name="id">Id to get seller</param>
        /// <returns><see cref="SellerModel"/> With data of seller</returns>
        public SellerModel GetSellerForId(int id)
        {
            SellerModel model = new SellerModel();

            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SEL_GETFORID, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(PARAM_ID, id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.Id             = Convert.ToInt32(reader[READER_ID]);
                        model.Name           = Convert.ToString(reader[READER_NAME]) ?? string.Empty;
                        model.Email          = Convert.ToString(reader[READER_EMAIL]) ?? string.Empty;
                        model.BirthDate      = Convert.ToDateTime(reader[READER_BITHDATE]);
                        model.BaseSalary     = Convert.ToDecimal(reader[READER_SALARY_BASE]);
                        model.Departament    = new DepartamentModel() { Id = Convert.ToInt32(reader[READER_DEPARTAMENT_ID]) };
                    }
                }
            }

            return model;
        }
        #endregion

        #region "Get All Seller"
        /// <summary>
        /// Get All Seller in database
        /// </summary>
        /// <returns><see cref="List{SellerModel}"/> With Data of sellers</returns>
        public List<SellerModel> GetAllSeller()
        {
            List<SellerModel> list = new List<SellerModel>();

            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SEL_GETALL, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(new SellerModel(Convert.ToInt32(reader[READER_ID]),
                                                 Convert.ToString(reader[READER_NAME]) ?? string.Empty,
                                                 Convert.ToString(reader[READER_EMAIL]) ?? string.Empty,
                                                 Convert.ToDateTime(reader[READER_BITHDATE]),
                                                 Convert.ToDecimal(reader[READER_SALARY_BASE]),
                                                 new DepartamentModel { Id = Convert.ToInt32(reader[READER_DEPARTAMENT_ID]) }));
                }
            }

            return list;
        }
        #endregion
    }
}
