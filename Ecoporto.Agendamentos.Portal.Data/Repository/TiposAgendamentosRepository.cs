using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using Dapper;
using Oracle.ManagedDataAccess.Client; 
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;
using System.Web.Mvc;

namespace Ecoporto.Agendamento.Portal.Data.Repository
{
    public class TiposAgendamentosRepository: UteisRepository, ITiposAgendamentosRepository  
    {
        public IEnumerable<TiposAgendamentos> GetListarTiposAgendamentos()
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT  ");
                    sb.AppendLine(" AUTONUM AS Id, ");
                    sb.AppendLine(" DESCRICAO as Descricao, ");
                    sb.AppendLine(" URL as Link, ");
                    sb.AppendLine(" IMAGEM as Image ");                    
                    sb.AppendLine(" FROM  ");
                    sb.AppendLine(" SGIPA.TB_SITES_AGENDAMENTOS ");
                    sb.AppendLine(" WHERE  ");
                    sb.AppendLine(" FLAG_ATIVO = 1 ");

                    //ViewBag.MainClass = "container-fluid top-content";

                    var query = _db.Query<TiposAgendamentos>(sb.ToString()).AsEnumerable();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
