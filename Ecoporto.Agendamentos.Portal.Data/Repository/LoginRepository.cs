using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client; 
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;

namespace Ecoporto.Agendamento.Portal.Data.Repository
{
    public class LoginRepository: UteisRepository, ILoginRepository 
    {
        public Login GetLoginUsuario(int id)
        {
            using (_db = new OracleConnection(Conexao))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT ");
                    sb.AppendLine(" IUSID As Id, ");
                    sb.AppendLine(" NVL(TRIM(UPPER(SUBSTR(B.IUSNOME, 1, INSTR(B.IUSNOME, ' ', 1, 1) - 1))), ' ') AS Nome, ");
                    sb.AppendLine(" IUSCPF As CPF, ");
                    sb.AppendLine(" IUSATIVO As Ativo, ");
                    sb.AppendLine(" IUSEMAIL As Email, ");
                    sb.AppendLine(" C.AUTONUM As DespachanteId, ");
                    sb.AppendLine(" SUBSTR(NVL(C.RAZAO, ' '), 0, 22) As DespachanteDescricao, ");
                    sb.AppendLine(" NVL(C.CGC, ' ') As DespachanteCNPJ, ");
                    sb.AppendLine(" NVL(TIACNPJ,'') AS CNPJTRANSPORTADORA,");
                    sb.AppendLine(" A.TIAID As TransportadoraID ");
                    sb.AppendLine(" FROM ");
                    sb.AppendLine(" INTERNET.TB_INT_ACESSO A ");
                    sb.AppendLine(" INNER JOIN ");
                    sb.AppendLine(" INTERNET.TB_INT_USER B ON A.USRID = B.IUSID ");
                    sb.AppendLine(" INNER JOIN ");
                    sb.AppendLine(" TB_CAD_PARCEIROS C ON A.TIACNPJ = C.CGC ");
                    sb.AppendLine(" WHERE ");
                    sb.AppendLine(" A.TIAID = " + id);

                    var query = _db.Query<Login>(sb.ToString()).FirstOrDefault();

                    return query; 
                }
                catch (Exception ex)
                {
                    throw ex; 
                }
            }
        }
    }
}
