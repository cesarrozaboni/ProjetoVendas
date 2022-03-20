using Domain.Enum;
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
        public const string SP_SLR_GETFORID = "SP_SLR_GEFORID";
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
        public const string READER_SELER_ID = "SALE_SELER_ID";
        #endregion

        public int CountSales()
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_COUNT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, 0));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(Int64)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public int InsertSales(SalesRecordModel model)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_INSERT, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_DATE, model.Date));
                cmd.Parameters.Add(new SqlParameter(PARAM_AMOUNT, model.Amount));
                cmd.Parameters.Add(new SqlParameter(PARAM_STATUS, model.Status));
                cmd.Parameters.Add(new SqlParameter(PARAM_SELER_ID, model.Seller.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, 0));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(Int64)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public int UpdateSales(SalesRecordModel model)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_UPDATE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, model.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_DATE, model.Date));
                cmd.Parameters.Add(new SqlParameter(PARAM_AMOUNT, model.Amount));
                cmd.Parameters.Add(new SqlParameter(PARAM_STATUS, model.Status));
                cmd.Parameters.Add(new SqlParameter(PARAM_SELER_ID, model.Seller.Id));
                cmd.Parameters.Add(new SqlParameter(PARAM_RETURN_VALUE, 0));
                cmd.Parameters[PARAM_RETURN_VALUE].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return (int)(Int64)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public int DeleteSales(int id)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_DELETE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));
                cmd.ExecuteNonQuery();

                return (int)cmd.Parameters[PARAM_RETURN_VALUE].Value;
            }
        }

        public SalesRecordModel GetSalesForId(int id)
        {
            SalesRecordModel model = new SalesRecordModel();

            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SP_SLR_GETFORID, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(PARAM_ID, id));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader[READER_ID]);
                        model.Date = Convert.ToDateTime(reader[READER_DATE]);
                        model.Amount = Convert.ToDecimal(reader[READER_AMOUNT]);
                        model.Seller.Id = Convert.ToInt32(reader[READER_SELER_ID]);
                    }
                }
            }

            return model;
        }

        public List<SalesRecordModel> GetAllSales()
        {
            List<SalesRecordModel> list = new List<SalesRecordModel>();

            using (SqlConnection conn = new SqlConnection(_conexao))
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
                                                      new SellerModel { Id = Convert.ToInt32(reader[READER_SELER_ID]) }
                                                      ));
                }
            }
            return list;
        }
    }
}
