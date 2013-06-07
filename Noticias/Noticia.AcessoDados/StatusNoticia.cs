using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class StatusNoticia : ICrud<Entidades.StatusNoticia>
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.StatusNoticia> Consultar(Entidades.StatusNoticia entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdStatusNoticia", entidade.IdStatus);
                objDados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spStatusNoticia");

                List<Entidades.StatusNoticia> objRetorno = new List<Entidades.StatusNoticia>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.StatusNoticia objNovoStatusNoticia = new Entidades.StatusNoticia();

                    objNovoStatusNoticia.IdStatus = objLinha["IdStatus"] != DBNull.Value ? Convert.ToInt32(objLinha["IdStatus"]) : 0;
                    objNovoStatusNoticia.Descricao = objLinha["Descricao"] != DBNull.Value ? Convert.ToString(objLinha["Descricao"]) : null;

                    objRetorno.Add(objNovoStatusNoticia);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.StatusNoticia entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spStatusNoticia");
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

        public string Alterar(Entidades.StatusNoticia entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdStatus > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdStatus", entidade.IdStatus);
                    objDados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spStatusNoticia");
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

        public string Excluir(Entidades.StatusNoticia entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdStatus > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdStatus", entidade.IdStatus);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spStatusNoticia");
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
