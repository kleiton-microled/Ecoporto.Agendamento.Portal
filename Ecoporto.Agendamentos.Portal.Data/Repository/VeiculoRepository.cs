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
    public class VeiculoRepository : UteisRepository,IVeiculosRepository
    {
        public int Cadastrar(Veiculos veiculo)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
               

                    StringBuilder sb = new StringBuilder();

                    var rastreador = veiculo.Rastreador ? 1 : 0;


                    sb.AppendLine("     INSERT INTO OPERADOR.TB_AG_VEICULOS");
                   sb.AppendLine("         ( ");
                    sb.AppendLine("             AUTONUM,");
                    sb.AppendLine("             ID_TRANSPORTADORA,");
                    sb.AppendLine("             ID_TIPO_CAMINHAO,");
                    sb.AppendLine("             PLACA_CAVALO,");
                    sb.AppendLine("             PLACA_CARRETA,");
                    sb.AppendLine("             CHASSI,");
                    sb.AppendLine("             RENAVAM,");
                    sb.AppendLine("             TARA,");
                    sb.AppendLine("             MODELO,");
                    sb.AppendLine("             COR,");
                    sb.AppendLine("             DT_CADASTRO,");
                    sb.AppendLine("             MARCA,ANO_FABRICACAO,ANO_MODELO,TANQUE,RASTREADOR,flag_ativo");
                    sb.AppendLine("         ) VALUES ( ");
                    sb.AppendLine("             OPERADOR.SEQ_AG_VEICULOS.NEXTVAL, ");
                    sb.AppendLine("             "+veiculo.TransportadoraId+",");
                    sb.AppendLine("             "+veiculo.TipoCaminhaoId+",");
                    sb.AppendLine("             '"+veiculo.Cavalo+"',");
                    sb.AppendLine("             '"+veiculo.Carreta+"',");
                    sb.AppendLine("             '"+veiculo.Chassi+"',");
                    sb.AppendLine("             '"+veiculo.Renavam+"',");
                    sb.AppendLine("             '"+veiculo.Tara+"',");
                    sb.AppendLine("             '"+veiculo.Modelo+"',");
                    sb.AppendLine("             '"+veiculo.Cor+"',");
                    sb.AppendLine("             SYSDATE,");
                    sb.AppendLine("             '" + veiculo.Marca + "',");
                    sb.AppendLine("             '" + veiculo.Ano_Fabricacao + "',");
                    sb.AppendLine("             '" + veiculo.Ano_Modelo + "',");
                    sb.AppendLine("             '" + veiculo.Tanque + "',");
                    sb.AppendLine("             '" + rastreador + "',1");                 
                    sb.AppendLine("         ) ");


                    var query = _db.Query<int>(sb.ToString()).FirstOrDefault();

                    return query;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Atualizar(Veiculos veiculo)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                  

                

                    StringBuilder sb = new StringBuilder();

                    var rastreador = veiculo.Rastreador ? 1 : 0;

                    sb.Clear();
                    sb.AppendLine("    UPDATE OPERADOR.TB_AG_VEICULOS ");
                    sb.AppendLine("        SET ");
                    sb.AppendLine("            ID_TIPO_CAMINHAO = " + veiculo.TipoCaminhaoId + ",");
                    //sb.AppendLine("            PLACA_CAVALO = '" + veiculo.Cavalo + "',");
                    //sb.AppendLine("            PLACA_CARRETA = '" + veiculo.Carreta + "',");
                    sb.AppendLine("            CHASSI = '" + veiculo.Chassi + "',");
                    sb.AppendLine("            RENAVAM = '" + veiculo.Renavam + "',");
                    sb.AppendLine("            TARA = '" + veiculo.Tara + "',");
                    sb.AppendLine("            MODELO = '" + veiculo.Modelo + "',");
                    sb.AppendLine("            COR = '" + veiculo.Cor + "',");

                    sb.AppendLine("            marca = '" + veiculo.Marca + "',");
                    sb.AppendLine("            rastreador = '" + rastreador + "',");
                    sb.AppendLine("            ano_modelo = '" + veiculo.Ano_Modelo + "',");
                    sb.AppendLine("            ano_fabricacao = '" + veiculo.Ano_Fabricacao + "',");
                    sb.AppendLine("           tanque = '" + veiculo.Tanque + "',");


                    sb.AppendLine("            DT_ULTIMA_ATUALIZACAO = SYSDATE");
                    sb.AppendLine("        WHERE");
                    sb.AppendLine("            AUTONUM = " + veiculo.Id +"" );


                    var query = _db.Query<int>(sb.ToString()).FirstOrDefault();

                    return query;
               
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

                    //_db.Execute(@"DELETE FROM OPERADOR.TB_AG_VEICULOS WHERE AUTONUM = :Id", parametros);

                    _db.Execute(@"update  OPERADOR.TB_AG_VEICULOS set flag_ativo = 0 WHERE AUTONUM = :Id", parametros);

                    _db.Close();
                    _db.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObterIdTransportadora(string cnpjtransp)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    StringBuilder sb = new StringBuilder();


                    sb.Clear();
                    sb.AppendLine("        SELECT ");
                    sb.AppendLine("         DISTINCT");
                    sb.AppendLine("         AUTONUM ");               
                    sb.AppendLine("     FROM");
                    sb.AppendLine("        operador.TB_CAD_TRANSPORTADORAS ");
                    sb.AppendLine("     where ");
                    sb.AppendLine("        cgc = '" + cnpjtransp + "'");

                    var query = _db.Query<int>(sb.ToString()).FirstOrDefault();
                    return query;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Veiculos> ObterVeiculos(int transportadoraId)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Clear();

                    sb.AppendLine("        SELECT ");
                    sb.AppendLine("         DISTINCT");
                    sb.AppendLine("         A.AUTONUM As Id,");
                    sb.AppendLine("         A.ID_TRANSPORTADORA As TransportadoraId,");
                    sb.AppendLine("         A.ID_TIPO_CAMINHAO As TipoCaminhaoId,");
                    sb.AppendLine("         A.PLACA_CAVALO As Cavalo,");
                    sb.AppendLine("         A.PLACA_CARRETA AS Carreta,");
                    sb.AppendLine("         A.Chassi,");
                    sb.AppendLine("            to_char(A.Renavam) as  Renavam ,");
                    sb.AppendLine("        to_char( A.Tara) as Tara,");
                    sb.AppendLine("         A.Modelo,");
                    sb.AppendLine("         A.Cor,");
                    sb.AppendLine("         B.DESCR As Descricao ");
                    sb.AppendLine("     FROM");
                    sb.AppendLine("         OPERADOR.TB_AG_VEICULOS A");
                    sb.AppendLine("     INNER JOIN");
                    sb.AppendLine("         OPERADOR.TB_TIPOS_CAMINHAO B ON A.ID_TIPO_CAMINHAO = B.AUTONUM");
                    sb.AppendLine("     WHERE");
                    sb.AppendLine("         A.ID_TRANSPORTADORA = " + transportadoraId + " and nvl(flag_ativo,1) = 1");

                    var query = _db.Query<Veiculos>(sb.ToString()).AsEnumerable();

                    return query;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        public int JaCadastrado(int transportadoraId, string Carreta, string Cavalo)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    StringBuilder sb = new StringBuilder();


                    sb.Clear();
                    sb.AppendLine(" SELECT ");
                    sb.AppendLine(" DISTINCT ");
                    sb.AppendLine("     count(0) ");
                    sb.AppendLine(" FROM ");
                    sb.AppendLine("      OPERADOR.TB_AG_VEICULOS  ");

                    sb.AppendLine("   WHERE");
                    sb.AppendLine("       PLACA_CAVALO = '" + Cavalo + "' and  PLACA_CARRETA = '" + Carreta + "'");
                    sb.AppendLine("   AND");
                    sb.AppendLine("      ID_TRANSPORTADORA = " + transportadoraId + "   ");

                    var query = _db.Query<int>(sb.ToString()).FirstOrDefault();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Veiculos> ObterUltimos5VeiculosAgendados(int transportadoraId)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add(name: "TransportadoraId", value: transportadoraId, direction: ParameterDirection.Input);


                    var query = _db.Query<Veiculos>(@"
                    SELECT
                        Id,    
                        Cavalo,
                        Carreta,
                        Cavalo || ' | ' || Carreta As Descricao
                    FROM
                        (
                            SELECT
                                DISTINCT
                                    A.AUTONUM As Id,
                                    A.ID_TRANSPORTADORA As TransportadoraId,
                                    A.ID_TIPO_CAMINHAO As TipoCaminhaoId,
                                    A.PLACA_CAVALO As Cavalo,
                                    A.PLACA_CARRETA AS Carreta,
                                    A.Chassi,
                                    A.Renavam,
                                    A.Tara,
                                    A.Modelo,
                                    A.Cor,
                                    C.DESCR As TipoCaminhaoDescricao
                            FROM
                                OPERADOR.TB_AG_VEICULOS A
                            INNER JOIN
                                OPERADOR.TB_AGENDAMENTO_CNTR B ON A.AUTONUM = B.AUTONUM_VEICULO AND A.ID_TRANSPORTADORA = B.AUTONUM_TRANSPORTADORA
                            INNER JOIN
                                OPERADOR.TB_TIPOS_CAMINHAO C ON A.ID_TIPO_CAMINHAO = C.AUTONUM
                            WHERE
                                A.ID_TRANSPORTADORA = :TransportadoraId
                        )
                    WHERE
                        ROWNUM <= 5
                    ORDER BY
                        Cavalo", parametros);

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

        public IEnumerable<Veiculos> ObterVeiculosPorPlaca(string descricao, int transportadoraId)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    _db.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Clear();
                    sb.AppendLine(" SELECT ");
                    sb.AppendLine(" DISTINCT ");
                    sb.AppendLine("       A.AUTONUM As Id,");
                    sb.AppendLine("    A.ID_TRANSPORTADORA As TransportadoraId,");
                    sb.AppendLine("    A.ID_TIPO_CAMINHAO As TipoCaminhaoId,");
                    sb.AppendLine("      A.PLACA_CAVALO As Cavalo,");
                    sb.AppendLine("      A.PLACA_CARRETA AS Carreta,");
                    sb.AppendLine("     A.Chassi,");
                    sb.AppendLine("     A.Renavam,");
                    sb.AppendLine("    A.Tara,");
                    sb.AppendLine("     A.Modelo,");
                    sb.AppendLine("    A.Cor,");
                    sb.AppendLine("    B.DESCR As TipoCaminhaoDescricao ");
                    sb.AppendLine(" FROM ");
                    sb.AppendLine("      OPERADOR.TB_AG_VEICULOS A ");
                    sb.AppendLine("  INNER JOIN");
                    sb.AppendLine("    OPERADOR.TB_TIPOS_CAMINHAO B ON A.ID_TIPO_CAMINHAO = B.AUTONUM     ");
                    sb.AppendLine("   WHERE");
                    sb.AppendLine("       (A.PLACA_CAVALO LIKE '%" + descricao + "%' OR PLACA_CARRETA LIKE '%" + descricao + "%')");
                    sb.AppendLine("   AND");
                    sb.AppendLine("      A.ID_TRANSPORTADORA = " + transportadoraId + "   ");


                    if (descricao == "")
                    {
                        sb.AppendLine(" AND  ROWNUM < 10");
                    };


                    var query = _db.Query<Veiculos>(sb.ToString()).AsEnumerable();

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

        public Veiculos ObterVeiculosPorId(int id)
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add(name: "Id", value: id, direction: ParameterDirection.Input);

                    var query = _db.Query<Veiculos>(@"
                    SELECT
                        A.AUTONUM As Id,
                        A.ID_TRANSPORTADORA As TransportadoraId,
                        A.ID_TIPO_CAMINHAO As TipoCaminhaoId,
                        A.PLACA_CAVALO As Cavalo,
                        A.PLACA_CARRETA AS Carreta,
                        A.Chassi,
                        A.Renavam,
                        A.Tara,
                        A.Modelo,
                        A.Cor,
                        B.Descr As TipoCaminhaoDescricao,a.marca,a.tanque,a.ano_fabricacao,a.ano_modelo,case when nvl(a.rastreador,0) = 1 then 1 else 0 end rastreador
                    FROM
                        OPERADOR.TB_AG_VEICULOS A
                    LEFT JOIN
                        OPERADOR.TB_TIPOS_CAMINHAO B ON A.ID_TIPO_CAMINHAO = B.AUTONUM
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

        public IEnumerable<TipoVeiculo> ObterTiposVeiculos()
        {
            try
            {
                using (_db = new OracleConnection(Conexao))
                {
                    _db.Open();

                    var query = _db.Query<TipoVeiculo>(@"SELECT AUTONUM As Id, DESCR As Descricao FROM OPERADOR.TB_TIPOS_CAMINHAO WHERE AUTONUM > 0 ORDER BY DESCR");


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
