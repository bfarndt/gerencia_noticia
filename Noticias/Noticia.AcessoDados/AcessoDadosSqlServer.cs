using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Noticia.AcessoDados.Properties;

namespace Noticia.AcessoDados
{

    /// <summary>
    /// Classe de acesso a dados para SQL Server
    /// </summary>
    public static class Dados
    {
        #region Cria a conexão

        private static SqlConnection Conexao;

        private static SqlConnection CriarConexao()
        {
            Conexao = new SqlConnection(Settings.Default.connDB);
            Conexao.Open();
            return Conexao;
        }

        public static void FecharConexao()
        {
            if (Conexao != null)
            {
                Conexao.Close();
                Conexao = null;
            }
        }

        #endregion

        #region Parâmetros que vão para banco

        private static SqlParameterCollection objParametros = new SqlCommand().Parameters;

        public static void LimparParametros()
        {
            objParametros.Clear();
        }

        public static void AdicionarParametros(string strNomeParametro, object objValor)
        {
            objParametros.Add(new SqlParameter(strNomeParametro, objValor));
        }

        #endregion

        #region Persistência - Inserir, Alterar, Excluir e Consultar

        //Inserir, Alterar e Excluir
        public static object ExecutarManipulacao(CommandType objTipo, string strSql)
        {
            try
            {
                //SP = Stored Procedure (Procedimento Armazenado no SQL Server)
                //strSql => é o comando SQL ou o nome da SP
                if (Conexao == null)
                    Conexao = CriarConexao();

                SqlConnection objConexao = Conexao;

                SqlCommand objComando = objConexao.CreateCommand();
                //Informa se será executada uma SP ou um texto SQL
                objComando.CommandType = objTipo;
                objComando.CommandText = strSql;
                objComando.CommandTimeout = 999999999; //Segundos

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
        public static DataTable ExecutaConsultar(CommandType objTipo, string strSql)
        {
            try
            {
                if (Conexao == null)
                    Conexao = CriarConexao();

                SqlConnection objConexao = Conexao;
                SqlCommand objComando = objConexao.CreateCommand();
                objComando.CommandType = objTipo;
                objComando.CommandText = strSql;
                objComando.CommandTimeout = 999999999;

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
