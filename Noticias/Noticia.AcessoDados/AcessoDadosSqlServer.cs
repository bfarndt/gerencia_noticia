using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Noticia.AcessoDados
{

    /// <summary>
    /// Classe de acesso a dados para SQL Server
    /// </summary>
    public class AcessoDadosSqlServer
    {
        #region Cria a conexão


        private SqlConnection CriarConexao()
        {
            return new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=Noticias;Integrated Security=True");
        }

        #endregion

        #region Parâmetros que vão para banco

        private SqlParameterCollection objParametros = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            objParametros.Clear();
        }

        public void AdicionarParametros(string strNomeParametro, object objValor)
        {
            objParametros.Add(new SqlParameter(strNomeParametro, objValor));
        }

        #endregion

        #region Persistência - Inserir, Alterar, Excluir e Consultar

        //Inserir, Alterar e Excluir
        public object ExecutarManipulacao(CommandType objTipo, string strSql)
        {
            try
            {
                //SP = Stored Procedure (Procedimento Armazenado no SQL Server)
                //strSql => é o comando SQL ou o nome da SP
                SqlConnection objConexao = CriarConexao();
                objConexao.Open();
                SqlCommand objComando = objConexao.CreateCommand();
                //Informa se será executada uma SP ou um texto SQL
                objComando.CommandType = objTipo;
                objComando.CommandText = strSql;
                objComando.CommandTimeout = 500; //Segundos

                //Adicionar os parâmetros para ir para o banco Sql Server
                foreach (SqlParameter objParametro in objParametros)
                    objComando.Parameters.Add(new SqlParameter(objParametro.ParameterName, objParametro.Value));

                return objComando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Consultar registros do banco de dados
        public DataTable ExecutaConsultar(CommandType objTipo, string strSql)
        {
            try
            {
                SqlConnection objConexao = CriarConexao();
                objConexao.Open();
                SqlCommand objComando = objConexao.CreateCommand();
                objComando.CommandType = objTipo;
                objComando.CommandText = strSql;
                objComando.CommandTimeout = 500;

                foreach (SqlParameter objParametro in objParametros)
                    objComando.Parameters.Add(new SqlParameter(objParametro.ParameterName, objParametro.Value));

                SqlDataAdapter objAdaptador = new SqlDataAdapter(objComando);
                DataTable objTabelaRecebeDados = new DataTable();

                objAdaptador.Fill(objTabelaRecebeDados);

                return objTabelaRecebeDados;
            }
            catch (Exception objErro)
            {
                throw new Exception(objErro.Message);
            }

        }

        #endregion
    }
}
