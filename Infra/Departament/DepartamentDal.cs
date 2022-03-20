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
        public const string PARAM_ID = "@P_ID";
        public const string PARAM_NAME = "@P_NAME";
        #endregion

        #region "Reader"
        public const string READER_ID = "DEP_ID";
        public const string READER_NAME = "DEP_NAME";
        #endregion

        public int CountDepartament()
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_COUNT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, 0));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(Int64)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public int InsertDepartament(DepartamentModel model)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_INSERT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_NAME, model.Name));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, 0));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(Int64)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }  
        }

        public int UpdateDepartament(DepartamentModel model)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_UPDATE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, model.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_NAME, model.Name));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, 0));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(Int64)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public int DeleteDepartament(int id)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_DEP_DELETE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));
                cmd.ExecuteNonQuery();

                return (int)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public DepartamentModel GetDepartamentForId(int id)
        {
            DepartamentModel model = new DepartamentModel();
            using (SqlConnection conn = new SqlConnection(_conexao))
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

        public List<DepartamentModel> GetAllDepartament()
        {
            List<DepartamentModel> list = new List<DepartamentModel>();

            using (SqlConnection conn = new SqlConnection(_conexao))
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

    }
}
