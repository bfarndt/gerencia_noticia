using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class Contratacao
    {
        public List<Entidades.Contratacao> Consultar(Entidades.Contratacao entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spContratacao");

                List<Entidades.Contratacao> objRetorno = new List<Entidades.Contratacao>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Contratacao objNovoContratacao = new Entidades.Contratacao();

                    objNovoContratacao.Usuario = new Entidades.Usuario()
                    {
                        IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0
                    };
                    objNovoContratacao.DataHora = objLinha["DataHora"] != DBNull.Value ? Convert.ToDateTime(objLinha["DataHora"]) : (DateTime?)null;

                    objRetorno.Add(objNovoContratacao);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Contratacao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@datDataHora", entidade.DataHora);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spContratacao");
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return intResultado.ToString();
                    else
                        throw new Exception(objRetorno.ToString());
                }
                else
                {
                    return "Não foi possível executar";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Alterar(Entidades.Contratacao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario != null && entidade.Usuario.IdUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@datDataHora", entidade.DataHora);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spContratacao");
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return intResultado.ToString();
                    else
                        throw new Exception(objRetorno.ToString());
                }
                else
                {
                    return "Não foi possível executar";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Excluir(Entidades.Contratacao entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario != null && entidade.Usuario.IdUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spContratacao");
                }


                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return intResultado.ToString();
                    else
                        throw new Exception(objRetorno.ToString());
                }
                else
                {
                    return "Não foi possível executar";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
