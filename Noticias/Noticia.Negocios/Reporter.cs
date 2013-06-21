using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Reporter : Usuario
    {
        AcessoDados.Noticia dalNoticia = new AcessoDados.Noticia();
        AcessoDados.Historico dalHistorico = new AcessoDados.Historico();
        AcessoDados.Imagem dalImagem = new AcessoDados.Imagem();
        AcessoDados.ImagemGravacao dalImagemGravacao = new AcessoDados.ImagemGravacao();
        AcessoDados.PalavraChave dalPalavraChave = new AcessoDados.PalavraChave();

        Negocios.Noticia NegNoticia = new Noticia();

        Negocios.Imagem NegImagem = new Imagem();

        public bool EditarNoticia(Entidades.Noticia noticia)
        {
            try
            {
                if (NegNoticia.TemTitulo(noticia) && NegNoticia.TemConteudo(noticia))
                {
                    //Executar update
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Alterar(noticia);
                    if (noticia.PalavrasChave != null)
                    {
                        strRetorno = dalPalavraChave.Excluir(new Entidades.PalavraChave() { Noticia = noticia });
                        foreach (var palavraChave in noticia.PalavrasChave)
                        {
                            strRetorno = dalPalavraChave.Inserir(palavraChave);
                        }
                    }

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        noticia.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = noticia;
                        historico.Usuario = Singleton.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Editada };

                        strRetorno = dalHistorico.Inserir(historico);
                    }

                    return intResult > 0;
                }
                else
                {
                    return false;
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

        public bool SubmeterNoticia(Entidades.Noticia noticia)
        {
            try
            {
                if (NegNoticia.TemTitulo(noticia) && NegNoticia.TemConteudo(noticia))
                {
                    //Executar update
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Alterar(noticia);
                    if (noticia.PalavrasChave != null)
                    {
                        strRetorno = dalPalavraChave.Excluir(new Entidades.PalavraChave() { Noticia = noticia });
                        foreach (var palavraChave in noticia.PalavrasChave)
                        {
                            strRetorno = dalPalavraChave.Inserir(palavraChave);
                        }
                    }

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        noticia.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = noticia;
                        historico.Usuario = Singleton.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Submetida };

                        strRetorno = dalHistorico.Inserir(historico);
                    }

                    return intResult > 0;
                }
                else
                {
                    return false;
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

        public bool SelecionarImagem(Entidades.Imagem imagem)
        {
            try
            {
                if (NegImagem.ValidarImagem(imagem))
                {
                    //Executar update
                    string strRetorno = string.Empty;

                    imagem.Selecionada = true;

                    strRetorno = dalImagem.Alterar(imagem);
                    strRetorno = dalImagemGravacao.Excluir(imagem.ImagemGravacao);
                    strRetorno = dalImagemGravacao.Inserir(imagem.ImagemGravacao);

                    int intResult = 0;
                    return (int.TryParse(strRetorno, out intResult));
                }
                else
                {
                    return false;
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
