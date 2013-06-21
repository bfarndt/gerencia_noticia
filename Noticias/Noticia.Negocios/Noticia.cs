using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Noticia
    {
        AcessoDados.Noticia dalNoticia = new AcessoDados.Noticia();
        AcessoDados.Historico dalHistorico = new AcessoDados.Historico();
        AcessoDados.NoticiaGrupoTrabalho dalNoticiaGrupoTrabalho = new AcessoDados.NoticiaGrupoTrabalho();
        AcessoDados.GrupoTrabalhoUsuario dalGrupoTrabalhoUsuario = new AcessoDados.GrupoTrabalhoUsuario();
        Negocios.Usuario NegUsuario = new Usuario();

        public bool TemTitulo(Entidades.Noticia noticia)
        {
            return noticia != null && !((string.IsNullOrWhiteSpace(noticia.Titulo)));
        }

        public bool TemConteudo(Entidades.Noticia noticia)
        {
            return noticia != null && !((string.IsNullOrWhiteSpace(noticia.Conteudo)));
        }

        public List<Entidades.Noticia> NoticiasParaEdicao()
        {
            try
            {
                if (NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Editar_Noticia))
                {
                    List<Entidades.Noticia> noticiasEdicao = new List<Entidades.Noticia>();

                    List<Entidades.StatusNoticia> statusConsulta = new List<Entidades.StatusNoticia>();
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Criada });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.GrupoVinculado });
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.ImagensAssociadas });

                    Entidades.Historico historico = new Entidades.Historico() { IdHistorico = null };
                    historico.Noticia = new Entidades.Noticia() { IdNoticia = null };
                    historico.Usuario = new Entidades.Usuario() { IdUsuario = null };

                    List<Entidades.Historico> historicos = dalHistorico.Consultar(historico, statusConsulta);
                    if (historicos.Count > 0)
                    {
                        noticiasEdicao = new List<Entidades.Noticia>();
                        foreach (var item in historicos)
                        {
                            noticiasEdicao.Add(item.Noticia);
                        }
                    }

                    return noticiasEdicao;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }


        }

        public List<Entidades.Noticia> NoticiasParaSubmissao()
        {
            try
            {
                if (NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Submeter_Noticia))
                {
                    List<Entidades.Noticia> noticiasSubmissao = new List<Entidades.Noticia>();

                    List<Entidades.StatusNoticia> statusConsulta = new List<Entidades.StatusNoticia>();
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Editada });

                    List<Entidades.Historico> historicos = dalHistorico.Consultar(null, statusConsulta);
                    if (historicos.Count > 0)
                    {
                        noticiasSubmissao = new List<Entidades.Noticia>();
                        foreach (var item in historicos)
                        {
                            noticiasSubmissao.Add(item.Noticia);
                        }
                    }

                    return noticiasSubmissao;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }


        }

        public List<Entidades.Noticia> NoticiasParaAvaliacao()
        {
            try
            {
                if (NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Avaliar_Noticia))
                {
                    List<Entidades.Noticia> noticiasAvaliacao = new List<Entidades.Noticia>();

                    List<Entidades.StatusNoticia> statusConsulta = new List<Entidades.StatusNoticia>();
                    statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Submetida });

                    List<Entidades.Historico> historicos = dalHistorico.Consultar(null, statusConsulta);
                    if (historicos.Count > 0)
                    {
                        noticiasAvaliacao = new List<Entidades.Noticia>();
                        foreach (var item in historicos)
                        {
                            noticiasAvaliacao.Add(item.Noticia);
                        }
                    }

                    return noticiasAvaliacao;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }


        }

        public List<Entidades.Noticia> NoticiasDoGrupoTrabalho()
        {
            try
            {
                if (NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Associar_Imagens))
                {
                    List<Entidades.Noticia> noticiasDoGrupo = new List<Entidades.Noticia>();

                    Entidades.GrupoTrabalhoUsuario consultaPorUsuario = new Entidades.GrupoTrabalhoUsuario();
                    consultaPorUsuario.Usuario = Singleton.UsuarioLogado;

                    Entidades.NoticiaGrupoTrabalho consultaPorGrupo;
                    foreach (var grupo in dalGrupoTrabalhoUsuario.Consultar(consultaPorUsuario))
                    {
                        consultaPorGrupo = new Entidades.NoticiaGrupoTrabalho();
                        consultaPorGrupo.GrupoTrabalho = grupo.GrupoTrabalho;

                        foreach (var noticia in dalNoticiaGrupoTrabalho.Consultar(consultaPorGrupo))
                        {
                            noticiasDoGrupo.Add(noticia.Noticia);
                        }
                    }

                    return noticiasDoGrupo;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }
        }
    }
}
