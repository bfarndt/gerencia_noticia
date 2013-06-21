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
        Negocios.Noticia NegNoticia = new Noticia();

        public bool AprovarNoticia(Entidades.Noticia noticia, string feedback)
        {
            try
            {
                if (NegNoticia.TemTitulo(noticia) && NegNoticia.TemConteudo(noticia))
                {
                    //Executar update
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Alterar(noticia);

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        noticia.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = noticia;
                        historico.Usuario = Singleton.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.Descricao = feedback;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Aprovada };

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

        public bool ReprovarNoticia(Entidades.Noticia noticia, string feedback)
        {
            try
            {
                //Executar update
                string strRetorno = string.Empty;

                strRetorno = dalNoticia.Alterar(noticia);

                int intResult = 0;
                if (int.TryParse(strRetorno, out intResult))
                {
                    noticia.IdNoticia = intResult;
                    Entidades.Historico historico = new Entidades.Historico();

                    historico.Noticia = noticia;
                    historico.Usuario = Singleton.UsuarioLogado;
                    historico.DataHora = DateTime.Now;
                    historico.Descricao = feedback;
                    historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Editada };

                    strRetorno = dalHistorico.Inserir(historico);
                }

                return intResult > 0;
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
