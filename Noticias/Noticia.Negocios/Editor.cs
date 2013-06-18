using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Editor : Usuario
    {
        AcessoDados.Noticia dalNoticia = new AcessoDados.Noticia();
        AcessoDados.Historico dalHistorico = new AcessoDados.Historico();

        public bool AprovarNoticia(string feedback)
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
                        historico.Descricao = feedback;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Aprovada };

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

        public bool ReprovarNoticia(string feedback)
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
                        historico.Descricao = feedback;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.GrupoVinculado };

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
    }
}
