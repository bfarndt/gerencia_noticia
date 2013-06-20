using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Noticia.AcessoDados
{
    public class Noticia : ICrud<Entidades.Noticia>
    {

        public List<Entidades.Noticia> Consultar(Entidades.Noticia entidade)
        {
            try
            {
                DataTable objDataTable = null;

                Dados.LimparParametros();
                Dados.AdicionarParametros("@vchAcao", "SELECIONAR");
                Dados.AdicionarParametros("@intIdNoticia", entidade.IdNoticia);
                Dados.AdicionarParametros("@vchTitulo", entidade.Titulo);
                Dados.AdicionarParametros("@vchConteudo", entidade.Conteudo);

                objDataTable = Dados.ExecutaConsultar(System.Data.CommandType.StoredProcedure, "spNoticia");

                List<Entidades.Noticia> objRetorno = new List<Entidades.Noticia>();

                if (objDataTable.Rows.Count <= 0)
                {
                    return objRetorno;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Entidades.Noticia objNovaNoticia = new Entidades.Noticia();

                    objNovaNoticia.IdNoticia = objLinha["IdNoticia"] != DBNull.Value ? Convert.ToInt32(objLinha["IdNoticia"]) : 0;
                    objNovaNoticia.Titulo = objLinha["Titulo"] != DBNull.Value ? Convert.ToString(objLinha["Titulo"]) : null;
                    objNovaNoticia.Conteudo = objLinha["Conteudo"] != DBNull.Value ? Convert.ToString(objLinha["Conteudo"]) : null;
                    objNovaNoticia.PalavrasChave = new AcessoDados.PalavraChave().Consultar(new Entidades.PalavraChave()
                    {
                        IdPalavraChave = null,
                        Noticia = objNovaNoticia,
                        PalavraChaveTexto = null
                    });

                    List<Entidades.StatusNoticia> statusConsulta = new List<Entidades.StatusNoticia>();

                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Aprovada });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Criada });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Editada });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.GrupoVinculado });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.ImagensAssociadas });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Submetida });

                    List<Entidades.Historico> historicos = new AcessoDados.Historico().Consultar(new Entidades.Historico()
                    {
                        Noticia = objNovaNoticia,
                        Usuario = new Entidades.Usuario() { IdUsuario = null }
                    }, statusConsulta);

                    if (historicos.Count > 0)
                    {
                        var statusNoticia = (from f in historicos
                                             select f).OrderByDescending(p => p.DataHora).First().StatusNoticia;

                        objNovaNoticia.StatusNoticia = statusNoticia;
                    }


                    objRetorno.Add(objNovaNoticia);
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Inserir(Entidades.Noticia entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null)
                {
                    Dados.AdicionarParametros("@vchAcao", "INSERIR");
                    Dados.AdicionarParametros("@vchTitulo", entidade.Titulo);
                    Dados.AdicionarParametros("@vchConteudo", entidade.Conteudo);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticia");
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

        public string Alterar(Entidades.Noticia entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdNoticia > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "ALTERAR");
                    Dados.AdicionarParametros("@intIdNoticia", entidade.IdNoticia);
                    Dados.AdicionarParametros("@vchTitulo", entidade.Titulo);
                    Dados.AdicionarParametros("@vchConteudo", entidade.Conteudo);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticia");
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

        public string Excluir(Entidades.Noticia entidade)
        {
            try
            {
                Dados.LimparParametros();
                object objRetorno = null;
                if (entidade != null && entidade.IdNoticia > 0)
                {
                    Dados.AdicionarParametros("@vchAcao", "DELETAR");
                    Dados.AdicionarParametros("@intIdNoticia", entidade.IdNoticia);

                    objRetorno = Dados.ExecutarManipulacao(CommandType.StoredProcedure, "spNoticia");
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
