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
                string strRetorno = string.Empty;


                int intResult = 0;
                Entidades.Historico historico = new Entidades.Historico();

                historico.Noticia = noticia;
                historico.Usuario = Singleton.UsuarioLogado;
                historico.DataHora = DateTime.Now;
                historico.Descricao = feedback;
                historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Aprovada };

                strRetorno = dalHistorico.Inserir(historico);


                return int.TryParse(strRetorno, out intResult);
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
                string strRetorno = string.Empty;


                int intResult = 0;
                Entidades.Historico historico = new Entidades.Historico();

                historico.Noticia = noticia;
                historico.Usuario = Singleton.UsuarioLogado;
                historico.DataHora = DateTime.Now;
                historico.Descricao = feedback;
                historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Editada };

                strRetorno = dalHistorico.Inserir(historico);

                return int.TryParse(strRetorno, out intResult);
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
