using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;

namespace Ecoporto.Agendamento.Portal.Data.Repository
{
    public class TiposAgendamentosRepository : UteisRepository, ITiposAgendamentosRepository
    {
        public IEnumerable<TiposAgendamentos> GetListarTiposAgendamentos()
        {
            const string query = @"
                                 SELECT  
                                    AUTONUM AS Id,
                                    TITULO AS Titulo,
                                    DESCRICAO AS Descricao,
                                    URL AS Link,
                                    IMAGEM AS Image
                                FROM  
                                    SGIPA.TB_SITES_AGENDAMENTOS
                                WHERE  
                                    FLAG_ATIVO = 1";
                                 
            try
            {
                using (var _db = new OracleConnection(Conexao))
                {
                    _db.Open();
                    return _db.Query<TiposAgendamentos>(query).AsEnumerable();
                }
            }
            catch (Exception)
            {
                // Lança a exceção sem modificar a stack trace original
                throw;
            }
        }

    }
}
