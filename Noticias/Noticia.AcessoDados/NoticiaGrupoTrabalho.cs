using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Noticia.AcessoDados
{
    public class NoticiaGrupoTrabalho
    {
        public List<Entidades.NoticiaGrupoTrabalho> Consultar(Entidades.NoticiaGrupoTrabalho entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                if (entidade.Noticia != null)
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                if (entidade.GrupoTrabalho != null)
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.GrupoTrabalho.IdGrupoTrabalho);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spNoticiaGrupoTrabalho");

                List<Entidades.NoticiaGrupoTrabalho> objRetorno = new List<Entidades.NoticiaGrupoTrabalho>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.NoticiaGrupoTrabalho objNovoNoticiaGrupoTrabalho = new Entidades.NoticiaGrupoTrabalho();

                    objNovoNoticiaGrupoTrabalho.Noticia = new Entidades.Noticia();
                    objNovoNoticiaGrupoTrabalho.Noticia.IdNoticia = objLinha["IdNoticia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;
                    objNovoNoticiaGrupoTrabalho.Noticia = new AcessoDados.Noticia().Consultar(objNovoNoticiaGrupoTrabalho.Noticia).First();

                    objNovoNoticiaGrupoTrabalho.GrupoTrabalho = new Entidades.GrupoTrabalho();
                    objNovoNoticiaGrupoTrabalho.GrupoTrabalho.IdGrupoTrabalho = objLinha["IdGrupoTrabalho"] != DBNull.Value ? Convert.ToInt32(objLinha["IdGrupoTrabalho"]) : 0;
                    objNovoNoticiaGrupoTrabalho.GrupoTrabalho = new AcessoDados.GrupoTrabalho().Consultar(objNovoNoticiaGrupoTrabalho.GrupoTrabalho).First();

                    objRetorno.Add(objNovoNoticiaGrupoTrabalho);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.NoticiaGrupoTrabalho entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.GrupoTrabalho.IdGrupoTrabalho);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticiaGrupoTrabalho");
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

        public string Alterar(Entidades.NoticiaGrupoTrabalho entidade)
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

        public string Excluir(Entidades.NoticiaGrupoTrabalho entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.Noticia != null && entidade.Noticia.IdNoticia > 0 &&
                    entidade.GrupoTrabalho != null && entidade.GrupoTrabalho.IdGrupoTrabalho > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdNoticia", entidade.Noticia.IdNoticia);
                    Dados.AdicionarParametros("@intIdGrupoTrabalho", entidade.GrupoTrabalho.IdGrupoTrabalho);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticiaGrupoTrabalho");
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
