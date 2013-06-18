using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class GrupoTrabalho
    {
        public List<Entidades.GrupoTrabalho> Consultar(Entidades.GrupoTrabalho entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.IdGrupoTrabalho);
                Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spGrupoTrabalho");

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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalho");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdGrupoTrabalho > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.IdGrupoTrabalho);
                    Dados.AdicionarParametros("@vchDescricao", entidade.Descricao);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalho");
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
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdGrupoTrabalho > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.IdGrupoTrabalho);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalho");
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
