using ProjetoVendas.Models.Departament;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Departament
{
    public class DepartamentDal : InfraBase
    {
        #region "Procedures"
        public const string SP_DEP_COUNT = "SP_DEP_COUNT";
        public const string SP_DEP_INSERT = "SP_DEP_INSERT";
        public const string SP_DEP_UPDATE = "SP_DEP_UPDATE";
        public const string SP_DEP_DELETE = "SP_DEP_DELETE";
        public const string SP_DEP_GETFORID = "SP_DEP_GETFORID";
        public const string SP_DEP_GETALL = "SP_DEP_GETALL";
        #endregion

        #region "Parameters"
        public const string PARAM_ID   = "@P_ID";
        public const string PARAM_NAME = "@P_NAME";
        #endregion

        #region "Reader"
        public const string READER_ID   = "DEP_ID";
        public const string READER_NAME = "DEP_NAME";
        #endregion

        #region "Count Departament"
        /// <summary>
        /// Get amount departament from database
        /// </summary>
        /// <returns></returns>
        public int CountDepartament()
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_COUNT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Insert Departament"
        /// <summary>
        /// Insert new departament in database
        /// </summary>
        /// <param name="model">model with values of departament</param>
        /// <returns> <see cref="int" /> number from new departament</returns>
        public int InsertDepartament(DepartamentModel model)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_INSERT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_NAME, model.Name));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }  
        }
        #endregion

        #region "Update Departament"
        /// <summary>
        /// Update departament in database
        /// </summary>
        /// <param name="model">Model with values of departament</param>
        /// <returns><see cref="int" /> 1=ok; 0=erro;</returns>
        public int UpdateDepartament(DepartamentModel model)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_UPDATE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, model.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_NAME, model.Name));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Delete Departament"
        /// <summary>
        /// Delete departament in database
        /// </summary>
        /// <param name="id">Id from departament</param>
        /// <returns><see cref="int" /> 1=ok; 0=erro;</returns>
        public int DeleteDepartament(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_DELETE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Get Departament Id"
        /// <summary>
        /// Get departament for id in database
        /// </summary>
        /// <param name="id">Id from departament</param>
        /// <returns><see cref="DepartamentModel"/> with values from departament</returns>
        public DepartamentModel GetDepartamentForId(int id)
        {
            DepartamentModel model = new DepartamentModel();
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_GETFORID, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) 
                    { 
                        model.Id   = Convert.ToInt32(reader[READER_ID]);
                        model.Name = Convert.ToString(reader[READER_NAME]) ?? string.Empty;
                    }
                }
            }

            return model;
        }
        #endregion

        #region "Get All Departament"
        /// <summary>
        /// Get all departament in database
        /// </summary>
        /// <returns><see cref="List{DepartamentModel}"/>With valus from all departament</returns>
        public List<DepartamentModel> GetAllDepartament()
        {
            List<DepartamentModel> list = new List<DepartamentModel>();

            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_GETALL, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                        list.Add(new DepartamentModel(Convert.ToInt32(reader[READER_ID]), Convert.ToString(reader[READER_NAME]) ?? string.Empty));
                }
            }
            return list;
        }
        #endregion
    }
}
