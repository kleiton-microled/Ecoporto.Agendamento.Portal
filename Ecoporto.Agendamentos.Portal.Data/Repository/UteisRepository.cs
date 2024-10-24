using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Ecoporto.Agendamento.Portal.Data.Repository
{
    public class UteisRepository
    {
      
        protected IDbConnection _db;
        public string Conexao = ConfigurationManager.ConnectionStrings["StringConexaoOracle"].ToString();
    }
}
