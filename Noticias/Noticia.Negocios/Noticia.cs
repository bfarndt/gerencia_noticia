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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }


        }

        public List<Entidades.Noticia> NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas()
        {
            try
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
                        if (noticia.Noticia.StatusNoticia != null)
                        {
                            if ((noticia.Noticia.StatusNoticia.IdStatus == (int)Entidades.StatusNoticiaEnum.Submetida) ||
                                (noticia.Noticia.StatusNoticia.IdStatus == (int)Entidades.StatusNoticiaEnum.Aprovada))
                            {
                                continue;
                            }
                        }
                        noticiasDoGrupo.Add(noticia.Noticia);
                    }
                }

                return noticiasDoGrupo;
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

        public List<Entidades.NoticiaImagem> NoticiasImagensAssociadas()
        {
            try
            {
                List<Entidades.NoticiaImagem> retorno = new List<Entidades.NoticiaImagem>();

                foreach (var noticia in this.NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas())
                {
                    foreach (var item in new AcessoDados.NoticiaImagem().Consultar(new Entidades.NoticiaImagem() { Noticia = noticia }))
                    {
                        if (item.Imagem.Selecionada.Value)
                            continue;

                        retorno.Add(new Entidades.NoticiaImagem() { Noticia = noticia, Imagem = item.Imagem });
                    }
                }

                return retorno;
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
