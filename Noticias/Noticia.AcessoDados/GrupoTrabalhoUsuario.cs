using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class GrupoTrabalhoUsuario
    {
        public List<Entidades.GrupoTrabalhoUsuario> Consultar(Entidades.GrupoTrabalhoUsuario entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                if (entidade.Usuario != null)
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                if (entidade.GrupoTrabalho != null)
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.GrupoTrabalho.IdGrupoTrabalho);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spGrupoTrabalhoUsuario");

                List<Entidades.GrupoTrabalhoUsuario> objRetorno = new List<Entidades.GrupoTrabalhoUsuario>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.GrupoTrabalhoUsuario objNovoGrupoTrabalhoUsuario = new Entidades.GrupoTrabalhoUsuario();

                    objNovoGrupoTrabalhoUsuario.Usuario = new Entidades.Usuario();
                    objNovoGrupoTrabalhoUsuario.Usuario.IdUsuario = objLinha["IdUsuario"] != DBNull.Value ? Convert.ToInt32(objLinha["IdUsuario"]) : 0;
                    objNovoGrupoTrabalhoUsuario.Usuario = new AcessoDados.Usuario().Consultar(objNovoGrupoTrabalhoUsuario.Usuario).First();

                    objNovoGrupoTrabalhoUsuario.GrupoTrabalho = new Entidades.GrupoTrabalho();
                    objNovoGrupoTrabalhoUsuario.GrupoTrabalho.IdGrupoTrabalho = objLinha["IdGrupoTrabalho"] != DBNull.Value ? Convert.ToInt32(objLinha["IdGrupoTrabalho"]) : 0;
                    objNovoGrupoTrabalhoUsuario.GrupoTrabalho = new AcessoDados.GrupoTrabalho().Consultar(objNovoGrupoTrabalhoUsuario.GrupoTrabalho).First();

                    objRetorno.Add(objNovoGrupoTrabalhoUsuario);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.GrupoTrabalhoUsuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.GrupoTrabalho.IdGrupoTrabalho);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalhoUsuario");
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

        public string Alterar(Entidades.GrupoTrabalhoUsuario entidade)
        {
            try
            {
                return "Utilize o excluir depois inserir";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Excluir(Entidades.GrupoTrabalhoUsuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Usuario != null && entidade.Usuario.IdUsuario > 0 &&
                    entidade.GrupoTrabalho != null && entidade.GrupoTrabalho.IdGrupoTrabalho > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.Usuario.IdUsuario);
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.GrupoTrabalho.IdGrupoTrabalho);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalhoUsuario");
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

        public string ExcluirPorUsuario(Entidades.Usuario entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdUsuario > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR_POR_USUARIO");
                    Dados.AdicionarParametros("@intIdUsuario", entidade.IdUsuario);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spGrupoTrabalhoUsuario");
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
