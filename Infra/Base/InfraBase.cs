using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace Infra
{
    public class InfraBase
    {
        private string _conexao;

        public const string PARAM_RETURN_VALUE = "@P_RETURN_VALUE";

        /// <summary>
        /// string connection to SqlServer
        /// </summary>
        public string Conexao { get => _conexao; private set => _conexao = value; }

        public InfraBase()
        {
            string assemblyPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(assemblyPath);
            _conexao = cfg.AppSettings.Settings["ConnectionString"].Value.ToString();
        }
    }
}