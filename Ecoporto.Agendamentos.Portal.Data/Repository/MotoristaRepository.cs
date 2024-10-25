using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;
using System.Data;


namespace Ecoporto.Agendamento.Portal.Data.Repository
{
 public   class MotoristaRepository : UteisRepository, IMotoristaRepository
    {
        public int Cadastrar(Motorista motorista)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var parametros = new DynamicParameters();

                    parametros.Add(name: "TransportadoraId", value: motorista.TransportadoraId, direction: ParameterDirection.Input);
                    parametros.Add(name: "Nome", value: motorista.Nome, direction: ParameterDirection.Input);
                    parametros.Add(name: "CNH", value: motorista.CNH, direction: ParameterDirection.Input);
                    parametros.Add(name: "ValidadeCNH", value: motorista.ValidadeCNH, direction: ParameterDirection.Input);
                    parametros.Add(name: "RG", value: motorista.RG, direction: ParameterDirection.Input);
                    parametros.Add(name: "CPF", value: motorista.CPF, direction: ParameterDirection.Input);
                    parametros.Add(name: "Celular", value: motorista.Celular, direction: ParameterDirection.Input);
                    parametros.Add(name: "Nextel", value: motorista.Nextel, direction: ParameterDirection.Input);
                    parametros.Add(name: "MOP", value: motorista.MOP, direction: ParameterDirection.Input);
                    //-----------------------------------------------------------------------------------------
                    parametros.Add(name: "DTNascimento", value: motorista.DT_NASCIMENTO, direction: ParameterDirection.Input);
                    parametros.Add(name: "Estrangeiro", value: motorista.Estrangeiro, direction: ParameterDirection.Input);
                    parametros.Add(name: "Bigrama", value: motorista.Bigrama, direction: ParameterDirection.Input);
                    parametros.Add(name: "CarteiraHabilitacao", value: motorista.Carteira_Habilitacao, direction: ParameterDirection.Input);
                    parametros.Add(name: "Passaport", value: motorista.Passaport, direction: ParameterDirection.Input);
                    parametros.Add(name: "DtPassaport", value: motorista.DT_Passaport, direction: ParameterDirection.Input);
                    parametros.Add(name: "Genero", value: motorista.Genero, direction: ParameterDirection.Input);
                    parametros.Add(name: "DTEMISSAO", value: motorista.Data_Emissao, direction: ParameterDirection.Input);
                    parametros.Add(name: "ORGAOEMISSOR", value: motorista.Orgao_Emissor, direction: ParameterDirection.Input);
                    //-----------------------------------------------------------------------------------------
                    parametros.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                   _db.Execute(@"
                        INSERT INTO OPERADOR.TB_AG_MOTORISTAS
                            ( 
                                AUTONUM,
                                ID_TRANSPORTADORA,
                                NOME,
                                CNH,
                                VALIDADE_CNH,
                                RG,
                                CPF,
                                CELULAR,
                                NEXTEL,
                                NUMERO_MOP,Passaporte,Dt_passaporte,dt_nascimento,bigrama,estrangeiro,carteira_habilitacao,genero,ORGAO_EMISSOR,DT_EMISSAO,flag_ativo 
                            ) VALUES ( 
                                OPERADOR.SEQ_AG_MOTORISTAS.NEXTVAL, 
                                :TransportadoraId,
                                :Nome,
                                :CNH,
                                :ValidadeCNH,
                                :RG,
                                :CPF,
                                :Celular,
                                :Nextel,
                                :MOP,
                                :Passaport,
                                :DtPassaport,
                                :DTNascimento,
                                :Bigrama,
                                :Estrangeiro,                                
                                :CarteiraHabilitacao,
                                :Genero,
                                :ORGAOEMISSOR,
                                :DTEMISSAO,1
                            ) RETURNING AUTONUM INTO :Id", parametros);

                   _db.Close();
                   _db.Dispose();

                    return parametros.Get<int>("Id");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(Motorista motorista)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
             
                    StringBuilder sb = new StringBuilder();

                    sb.Clear();

                    sb.AppendLine("   UPDATE OPERADOR.TB_AG_MOTORISTAS");
                    sb.AppendLine("       SET ");
                    sb.AppendLine("     NOME = '" + motorista.Nome + "',");
                    sb.AppendLine("     CNH = '" + motorista.CNH + "',");
                    sb.AppendLine("     VALIDADE_CNH = '" + motorista.ValidadeCNH + "',");
                    sb.AppendLine("     RG = '" + motorista.RG + "',");
                    sb.AppendLine("     CPF = '" + motorista.CPF + "',");
                    sb.AppendLine("     CELULAR = '" + motorista.Celular + "',");
                    sb.AppendLine("     NEXTEL = '" + motorista.Nextel + "',");
                    sb.AppendLine("     NUMERO_MOP = '" + motorista.MOP + "',");
                    sb.AppendLine("     dt_nascimento = '" + motorista.DT_NASCIMENTO + "',");
                    sb.AppendLine("    orgao_emissor  = '" + motorista.Orgao_Emissor + "',");
                    sb.AppendLine("     DATA_EMISSAO = '" + motorista.Data_Emissao + "',");
                   
                    sb.AppendLine("           DT_ULTIMA_ATUALIZACAO = SYSDATE");
                    sb.AppendLine("       WHERE ");
                    sb.AppendLine("           AUTONUM = " + motorista.Id + "");

                    var query =_db.Query(sb.ToString()).FirstOrDefault();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(int id)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var parametros = new DynamicParameters();
                    parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                    //_db.Execute(@"DELETE FROM OPERADOR.TB_AG_MOTORISTAS WHERE AUTONUM = :Id", parametros);
                    _db.Execute(@"update  OPERADOR.TB_AG_MOTORISTAS set flag_ativo = 0 WHERE AUTONUM = :Id", parametros);
                    _db.Close();
                   _db.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<Motorista> ObterMotoristas(int transportadoraId)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var parametros = new DynamicParameters();
                    parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                  SELECT
                       DISTINCT
                        AUTONUM As Id,
                        ID_TRANSPORTADORA As TransportadoraId,
                        NOME,
                        nvl(CNH,'0') CNH,
                        nvl(PASSAPORTe,'0') PASSAPORT ,
                       to_char(VALIDADE_CNH,'dd/MM/YYYY') As ValidadeCNH,
                        RG,
                        nvl(CPF,'0') CPF ,
                        Celular,
                        nvl(ESTRANGEIRO,0) ESTRANGEIRO  ,
                        nvl(CARTEIRA_HABILITACAO,'0') CARTEIRA_HABILITACAO ,
                        Nextel,
                        NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS
                    WHERE
                        ID_TRANSPORTADORA = :TransportadoraId
                           and nvl(flag_ativo,1) = 1
                    ORDER BY
                             nome ", parametros);

                   _db.Close();
                   _db.Dispose();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Motorista> ObterUltimos5MotoristasAgendados(int transportadoraId)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var parametros = new DynamicParameters();
                    parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                    SELECT
                        Id,    
                        NOME,
                        CNH,
                        NOME || ' - ' || CNH As Descricao
                    FROM
                        (
                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.ID_TRANSPORTADORA As TransportadoraId,
                                    A.NOME,
                                    A.CNH,
                                    A.VALIDADE_CNH As ValidadeCNH,
                                    A.RG,
                                    A.CPF,
                                    A.Celular,
                                    A.Nextel,
                                    A.NUMERO_MOP As MOP
                            FROM
                                OPERADOR.TB_AG_MOTORISTAS A
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CNTR B ON A.AUTONUM = B.AUTONUM_MOTORISTA AND A.ID_TRANSPORTADORA = B.AUTONUM_TRANSPORTADORA
                            WHERE
                                A.ID_TRANSPORTADORA = :TransportadoraId
                        )
                    WHERE
                        ROWNUM <= 5
                    ORDER BY
                        NOME", parametros);

                   _db.Close();
                   _db.Dispose();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Motorista> ObterMotoristasPorNomeOuCNH(string descricao, int transportadoraId)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var criterioDescricao = "%" + descricao.ToUpper() + "%";

                    var parametros = new DynamicParameters();

                    parametros.Add(name: "Nome", value: criterioDescricao, direction: ParameterDirection.Input);
                    parametros.Add(name: "CNH", value: criterioDescricao, direction: ParameterDirection.Input);
                    parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                    SELECT
                        AUTONUM As Id,
                        ID_TRANSPORTADORA As TransportadoraId,
                        NOME,
                        CNH,
                        VALIDADE_CNH As ValidadeCNH,
                        RG,
                        CPF,
                        Celular,
                        Nextel,
                        NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS
                    WHERE
                        (NOME LIKE :Nome OR CNH LIKE :CNH)
                    AND
                        ID_TRANSPORTADORA = :TransportadoraId
                    AND 
                        ROWNUM < 10
                    ORDER BY
                        VALIDADE_CNH DESC", parametros);

                   _db.Close();
                   _db.Dispose();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Motorista ObterMotoristaPorId(int id)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.NOME,
                        A.CNH,
                        A.VALIDADE_CNH As ValidadeCNH,
                        A.RG,
                        A.CPF,
                        A.Celular,
                        A.Nextel,
                        A.NUMERO_MOP As MOP,
                        nvl(   TO_CHAR(A.DT_ULTIMA_ATUALIZACAO,'dd/MM/yyyy HH24:MI'),'') As UltimaAlteracao,
                        a.estrangeiro ,
                        a.dt_passaporte as dt_passaport,
                        a.passaporte as passaport,
                        a.carteira_habilitacao,
                        a.ORGAO_EMISSOR,
                         nvl(TO_CHAR( a.DATA_EMISSAO,'dd/MM/yyyy'),'') as Data_Emissao,
                        a.bigrama,
                       
                        a.estrangeiro,
                        a.genero,
                        DT_NASCIMENTO,
                        NVL(B.FLAG_INATIVO, 0) As INATIVO
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS A
                    LEFT JOIN
                        OPERADOR.TB_MOTORISTAS B ON REPLACE(REPLACE(A.CPF,'.',''),'-','') = REPLACE(REPLACE(B.CPF,'.',''),'-','')
                        and  replace(REPLACE(REPLACE(A.CNH,'.',''),'-',''),'/','') = REPLACE(REPLACE(REPLACE(B.CNH,'.',''),'-',''),'','')
                    WHERE
                        A.AUTONUM = :Id", parametros).FirstOrDefault();

                   _db.Close();
                   _db.Dispose();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Motorista ObterMotoristaPorCNH(string cnh, int transportadoraId)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                    var parametros = new DynamicParameters();

                    parametros.Add(name: "CNH", value: cnh, direction: ParameterDirection.Input);
                    parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                    SELECT
                        AUTONUM As Id,
                        ID_TRANSPORTADORA As TransportadoraId,
                        NOME,
                        CNH,
                        VALIDADE_CNH As ValidadeCNH,
                        RG,
                        CPF,
                        Celular,
                        Nextel,
                        NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS
                    WHERE
                        CNH = :CNH
                    AND
                        ID_TRANSPORTADORA = :TransportadoraId", parametros).FirstOrDefault();

                   _db.Close();
                   _db.Dispose();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Motorista ObterMotoristaPorCPF(string cpf, int transportadoraId)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var parametros = new DynamicParameters();

                    parametros.Add(name: "CPF", value: cpf, direction: ParameterDirection.Input);
                    parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                    SELECT
                        AUTONUM As Id,
                        ID_TRANSPORTADORA As TransportadoraId,
                        NOME,
                        CNH,
                        VALIDADE_CNH As ValidadeCNH,
                        RG,
                        CPF,
                        Celular,
                        Nextel,
                        NUMERO_MOP As MOP
                    FROM
                        OPERADOR.TB_AG_MOTORISTAS
                    WHERE
                        CPF = :CPF
                    AND
                        ID_TRANSPORTADORA = :TransportadoraId", parametros).FirstOrDefault();

                   _db.Close();
                   _db.Dispose();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Motorista ObterMotoristaChronosPorCNH(string cnh)
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    var parametros = new DynamicParameters();
                    parametros.Add(name: "CNH", value: cnh, direction: ParameterDirection.Input);

                    var query =_db.Query<Motorista>(@"
                    SELECT        
                        NOME,
                        CNH,
                        VALIDADE_CNH As ValidadeCNH,
                        RG,
                        CPF,
                        REPLACE(REPLACE(TEL_CELULAR,'(__) _____-____',''),'(00) 00000-0000','') As Celular,
                        DT_ULTIMA_ATUALIZACAO As UltimaAlteracao
                    FROM
                        OPERADOR.TB_MOTORISTAS
                    WHERE
                        CNH = :CNH", parametros).FirstOrDefault();

                   _db.Close();
                   _db.Dispose();


                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Paises> ObterPaises()
        {
            try
            {
               using (_db = new OracleConnection(Conexao))
                {
                   _db.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Clear();
                    sb.Append("SELECT DESCR, BIGRAMA FROM SGIPA.TB_CAD_PAISES ");

                    var query =_db.Query<Paises>(sb.ToString()).AsEnumerable();

                   _db.Close();
                   _db.Dispose();


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
