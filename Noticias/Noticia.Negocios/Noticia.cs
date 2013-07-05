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

        public List<Entidades.Noticia> NoticiasParaEdicao(Entidades.Noticia consultaTitulo)
        {
            try
            {
                var retorno = from f in this.NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas()
                              where f.StatusNoticia.IdStatus == (int)Entidades.StatusNoticiaEnum.GrupoVinculado ||
                                    f.StatusNoticia.IdStatus == (int)Entidades.StatusNoticiaEnum.ImagensAssociadas
                              select f;

                if (consultaTitulo != null && !string.IsNullOrWhiteSpace(consultaTitulo.Titulo))
                {
                    var found = from f in retorno
                                where f.Titulo.Contains(consultaTitulo.Titulo)
                                select f;
                    if (found != null && found.Count() > 0)
                        return found.ToList<Entidades.Noticia>();

                }

                return retorno.ToList<Entidades.Noticia>();
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

        public List<Entidades.Noticia> NoticiasParaSubmissao(Entidades.Noticia consultaTitulo)
        {
            try
            {
                var retorno = from f in this.NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas()
                              where f.StatusNoticia.IdStatus == (int)Entidades.StatusNoticiaEnum.Editada
                              select f;

                if (consultaTitulo != null && !string.IsNullOrWhiteSpace(consultaTitulo.Titulo))
                {
                    var found = from f in retorno
                                where f.Titulo.Contains(consultaTitulo.Titulo)
                                select f;
                    if (found != null && found.Count() > 0)
                        return found.ToList<Entidades.Noticia>();

                }

                return retorno.ToList<Entidades.Noticia>();
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

        public List<Entidades.Noticia> NoticiasParaAvaliacao(Entidades.Noticia consultaTitulo)
        {
            try
            {
                var retorno = from f in this.NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas()
                              where f.StatusNoticia.IdStatus == (int)Entidades.StatusNoticiaEnum.Submetida
                              select f;

                if (consultaTitulo != null && !string.IsNullOrWhiteSpace(consultaTitulo.Titulo))
                {
                    var found = from f in retorno
                                where f.Titulo.Contains(consultaTitulo.Titulo)
                                select f;
                    if (found != null && found.Count() > 0)
                        return found.ToList<Entidades.Noticia>();

                }

                return retorno.ToList<Entidades.Noticia>();
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

        public List<Entidades.NoticiaImagem> NoticiasImagensSelecionadas()
        {
            try
            {
                List<Entidades.NoticiaImagem> retorno = new List<Entidades.NoticiaImagem>();

                foreach (var noticia in this.NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas())
                {
                    foreach (var item in new AcessoDados.NoticiaImagem().Consultar(new Entidades.NoticiaImagem() { Noticia = noticia }))
                    {
                        if (!item.Imagem.Selecionada.Value)
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

        public List<Entidades.NoticiaImagem> ImagensDeNoticiasAssociadas()
        {
            try
            {
                List<Entidades.NoticiaImagem> imagensAssociadas = new List<Entidades.NoticiaImagem>();

                Entidades.GrupoTrabalhoUsuario consultaPorUsuario = new Entidades.GrupoTrabalhoUsuario();
                consultaPorUsuario.Usuario = Singleton.UsuarioLogado;

                Entidades.NoticiaGrupoTrabalho consultaPorGrupo;
                Entidades.NoticiaImagem consultaPorNoticia;

                foreach (var grupo in dalGrupoTrabalhoUsuario.Consultar(consultaPorUsuario))
                {
                    consultaPorGrupo = new Entidades.NoticiaGrupoTrabalho();
                    consultaPorGrupo.GrupoTrabalho = grupo.GrupoTrabalho;

                    foreach (var noticia in dalNoticiaGrupoTrabalho.Consultar(consultaPorGrupo))
                    {
                        consultaPorNoticia = new Entidades.NoticiaImagem();
                        consultaPorNoticia.Noticia = noticia.Noticia;

                        foreach (var imagem in new AcessoDados.NoticiaImagem().Consultar(consultaPorNoticia))
                        {
                            if (imagem.Imagem.Selecionada.Value)
                                continue;

                            var consulta = new AcessoDados.ImagemArquivo().Consultar(new Entidades.ImagemArquivo() { Imagem = imagem.Imagem });
                            if (consulta.Count > 0)
                                imagem.Imagem.Legenda = consulta.First().NomeArquivo;

                            imagensAssociadas.Add(imagem);
                        }
                    }
                }

                return imagensAssociadas;
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

        public Entidades.Noticia NoticiaPorId(int IdNoticia)
        {
            try
            {
                var noticias = new AcessoDados.Noticia().Consultar(new Entidades.Noticia() { IdNoticia = IdNoticia });

                var found = from f in noticias
                            where f.IdNoticia == IdNoticia
                            select f;

                if (found != null && found.Count() > 0)
                    return found.First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidades.PalavraChave> PalavrasChaveDaNoticia(Entidades.Noticia noticia)
        {
            try
            {
                List<Entidades.PalavraChave> retorno = new AcessoDados.PalavraChave().Consultar(new Entidades.PalavraChave() { IdPalavraChave = null, PalavraChaveTexto = null, Noticia = noticia });

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

        public List<Entidades.Historico> GerarRelatorio(Entidades.Noticia consultaNoticia, List<Entidades.StatusNoticia> VariosStatusNoticia)
        {
            try
            {
                List<Entidades.Historico> retorno = dalHistorico.Consultar(new Entidades.Historico()
                {
                    IdHistorico = null,
                    Noticia = new Entidades.Noticia()
                    {
                        IdNoticia = null
                    },
                    Usuario = new Entidades.Usuario() { IdUsuario = null }
                },
                VariosStatusNoticia);

                if (retorno.Count > 0)
                {
                    foreach (var item in retorno)
                    {
                        item.Noticia = NoticiaPorId(item.IdNoticia);
                    }
                }

                if (consultaNoticia != null && !string.IsNullOrWhiteSpace(consultaNoticia.Titulo))
                {
                    var found = from f in retorno
                                where f.Noticia != null &&
                                      f.Noticia.Titulo != null &&
                                      f.Noticia.Titulo.Contains(consultaNoticia.Titulo)
                                select f;
                    if (found != null && found.Count() > 0)
                        return found.ToList<Entidades.Historico>();
                    else
                        return new List<Entidades.Historico>();
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

        public List<Entidades.StatusNoticia> TodosStatus()
        {
            try
            {
                return new AcessoDados.StatusNoticia().Consultar(new Entidades.StatusNoticia() { IdStatus = null, Descricao = "%" });
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
