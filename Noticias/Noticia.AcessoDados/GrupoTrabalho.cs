using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class GrupoTrabalho
    {
        AcessoDadosSqlServer objDados = new AcessoDadosSqlServer();

        public List<Entidades.GrupoTrabalho> Consultar(Entidades.GrupoTrabalho entidade)
        {
            try
            {
                DataTable objDataTable = null;

                objDados.LimparParametros();
                objDados.AdicionarParametros("@vchAcao", "SELECIONAR");
                objDados.AdicionarParametros("@intIdGrupoTrabalho", entidade.IdGrupoTrabalho);
                objDados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                objDataTable = objDados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spGrupoTrabalho");

                List<Entidades.GrupoTrabalho> objRetorno = new List<Entidades.GrupoTrabalho>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.GrupoTrabalho objNovoGrupoTrabalho = new Entidades.GrupoTrabalho();

                    objNovoGrupoTrabalho.IdGrupoTrabalho = objLinha["IdGrupoTrabalho"] != DBNull.Value ? Convert.ToInt32(objLinha["IdGrupoTrabalho"]) : 0;
                    objNovoGrupoTrabalho.Descricao = objLinha["Descricao"] != DBNull.Value ? Convert.ToString(objLinha["Descricao"]) : null;

                    objRetorno.Add(objNovoGrupoTrabalho);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.GrupoTrabalho entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    objDados.AdicionarParametros("@vchAcao", "INSERIR");
                    objDados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalho");
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

        public string Alterar(Entidades.GrupoTrabalho entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdGrupoTrabalho > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "ALTERAR");
                    objDados.AdicionarParametros("@intIdGrupoTrabalho", entidade.IdGrupoTrabalho);
                    objDados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalho");
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

        public string Excluir(Entidades.GrupoTrabalho entidade)
        {
            try
            {
                objDados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdGrupoTrabalho > 0)
                {
                    objDados.AdicionarParametros("@vchAcao", "DELETAR");
                    objDados.AdicionarParametros("@intIdGrupoTrabalho", entidade.IdGrupoTrabalho);

                    objRetorno = objDados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalho");
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
