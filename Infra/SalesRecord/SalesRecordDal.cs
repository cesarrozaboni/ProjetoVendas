using Domain.Enums;
using Domain.Sales;
using Domain.Seller;
using System.Data;
using System.Data.SqlClient;

namespace Infra.SalesRecord
{
    public class SalesRecordDal : InfraBase
    {
        #region "Procedures"
        public const string SP_SLR_INSERT   = "SP_SLR_INSERT";
        public const string SP_SLR_UPDATE   = "SP_SLR_UPDATE";
        public const string SP_SLR_DELETE   = "SP_SLR_DELETE";
        public const string SP_SLR_GETFORID = "SP_SLR_GETFORID";
        public const string SP_SLR_GETALL   = "SP_SLR_GETALL";
        public const string SP_SLR_COUNT    = "SP_SLR_COUNT";
        #endregion

        #region "Parameters"
        public const string PARAM_ID       = "@P_ID";
        public const string PARAM_DATE     = "@P_DATE";
        public const string PARAM_AMOUNT   = "@P_AMOUNT";
        public const string PARAM_STATUS   = "@P_STATUS";
        public const string PARAM_SELER_ID = "@P_SELER_ID";
        #endregion

        #region "Reader"
        public const string READER_ID       = "SALE_ID";
        public const string READER_DATE     = "SALE_DATE";
        public const string READER_AMOUNT   = "SALE_AMOUNT";
        public const string READER_STATUS   = "SALE_STATUS";
        public const string READER_SELLER_ID = "SALE_SELLER_ID";
        #endregion

        #region "Count Sales"
        /// <summary>
        /// Count amount sales in database
        /// </summary>
        /// <returns><see cref="int"/> amount sales in database</returns>
        public int CountSales()
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_COUNT, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Insert Sales"
        /// <summary>
        /// Insert new sales in departament
        /// </summary>
        /// <param name="model">model to insert new sales in database</param>
        /// <returns><see cref="int"/> rows affected</returns>
        public int InsertSales(SalesRecordModel model)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_INSERT, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(PARAM_DATE, model.Date));
                cmd.Parameters.Add(new SqlParameter(PARAM_AMOUNT, model.Amount));
                cmd.Parameters.Add(new SqlParameter(PARAM_STATUS, model.Status));
                cmd.Parameters.Add(new SqlParameter(PARAM_SELER_ID, model.Seller.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Update Sales"
        /// <summary>
        /// Update sale in database
        /// </summary>
        /// <param name="model">model with new value</param>
        /// <returns><see cref="int"/ rows affected></returns>
        public int UpdateSales(SalesRecordModel model)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_UPDATE, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(PARAM_ID, model.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_DATE, model.Date));
                cmd.Parameters.Add(new SqlParameter(PARAM_AMOUNT, model.Amount));
                cmd.Parameters.Add(new SqlParameter(PARAM_STATUS, (int)model.Status));
                cmd.Parameters.Add(new SqlParameter(PARAM_SELER_ID, model.Seller.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, default(int)));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(long)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Delete Sales"
        /// <summary>
        /// Delete sale in database
        /// </summary>
        /// <param name="id">id with value to delete</param>
        /// <returns><see cref="int"/> rows affected</returns>
        public int DeleteSales(int id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_DELETE, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));
                cmd.ExecuteNonQuery();

                return (int)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }
        #endregion

        #region "Get Sales For Id"
        /// <summary>
        /// Get Sales For Id in database
        /// </summary>
        /// <param name="id">id to search sales</param>
        /// <returns><see cref="SalesRecordModel"/> object with values</returns>
        public SalesRecordModel GetSalesForId(int id)
        {
            SalesRecordModel model = new SalesRecordModel();

            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_GETFORID, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.Id        = Convert.ToInt32(reader[READER_ID]);
                        model.Date      = Convert.ToDateTime(reader[READER_DATE]);
                        model.Amount    = Convert.ToDecimal(reader[READER_AMOUNT]);
                        model.Seller    = new SellerModel { Id = Convert.ToInt32(reader[READER_SELLER_ID]) };
                    }
                }
            }
            return model;
        }
        #endregion

        #region "Get All Sales"
        /// <summary>
        /// Get All Sales in database
        /// </summary>
        /// <returns><see cref="List{SalesRecordModel}"/>List of SalesRecordModel</returns>
        public List<SalesRecordModel> GetAllSales()
        {
            List<SalesRecordModel> list = new List<SalesRecordModel>();

            using (SqlConnection conn = new SqlConnection(Conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_GETALL, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(new SalesRecordModel(Convert.ToInt32(reader[READER_ID]),
                                                      Convert.ToDateTime(reader[READER_DATE]),
                                                      Convert.ToDecimal(reader[READER_AMOUNT]),
                                                      (SaleStatusModel)Convert.ToInt32(reader[READER_STATUS]),
                                                      new SellerModel { Id = Convert.ToInt32(reader[READER_SELLER_ID]) }));
                }
            }
            return list;
        }
        #endregion

    }
}
