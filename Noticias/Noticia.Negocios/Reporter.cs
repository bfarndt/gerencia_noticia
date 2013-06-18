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

        Negocios.Imagem NegImagem = new Imagem();

        public bool SubmeterEdicao()
        {
            try
            {
                if (Noticia.ValidarNoticia())
                {
                    //Executar update
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Alterar(Sessao.NoticiaAtual);

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        Sessao.NoticiaAtual.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = Sessao.NoticiaAtual;
                        historico.Usuario = Sessao.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Editada };

                        strRetorno = dalHistorico.Inserir(historico);
                    }

                    return intResult > 0;
                }
                else
                {
                    Sessao.NoticiaAtual = null;
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

        public bool SubmeterNoticia()
        {
            try
            {
                if (Noticia.ValidarNoticia())
                {
                    //Executar update
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Alterar(Sessao.NoticiaAtual);

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        Sessao.NoticiaAtual.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = Sessao.NoticiaAtual;
                        historico.Usuario = Sessao.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Submetida };

                        strRetorno = dalHistorico.Inserir(historico);
                    }

                    return intResult > 0;
                }
                else
                {
                    Sessao.NoticiaAtual = null;
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

                    int intResult = 0;
                    return (int.TryParse(strRetorno, out intResult));
                }
                else
                {
                    Sessao.NoticiaAtual = null;
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
