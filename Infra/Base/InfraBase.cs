using System.Data.SqlClient;

namespace Infra
{
    public class InfraBase
    {
        public const string _conexao = "Data Source=ROZABONI-02\\SQLEXPRESS3;Initial Catalog=SistemaVendas;Integrated Security=true";

        public const string PARAM_RETURN_VALUE = "@P_RETURN_VALUE";


        public void testeConexao()
        {
            
            
            //SqlConnection conn = new SqlConnection(AppName);
            //conn.Open();

            //SqlCommand command = new SqlCommand("Select 1 as id", conn);

            ////command.Parameters.AddWithValue("@zip","india");
            ////int result = command.ExecuteNonQuery();

            //// result gives the -1 output.. but on insert its 1
            //using (SqlDataReader reader = command.ExecuteReader())
            //{
            //    if (reader.Read())
            //    {
            //        var teste = reader["id"];
            //    }
            //    // iterate your results here


            //}

            //conn.Close();
        }
    }
}