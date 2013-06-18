using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Diretor : Usuario
    {
        AcessoDados.Noticia dalNoticia = new AcessoDados.Noticia();
        AcessoDados.Historico dalHistorico = new AcessoDados.Historico();
        AcessoDados.NoticiaGrupoTrabalho dalNoticiaGrupoTrabalho = new AcessoDados.NoticiaGrupoTrabalho();

        public bool CriarNoticia()
        {
            try
            {
                if (Noticia.ValidarNoticia())
                {
                    //Executar insert
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Inserir(Sessao.NoticiaAtual);

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        Sessao.NoticiaAtual.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = Sessao.NoticiaAtual;
                        historico.Usuario = Sessao.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Criada };

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

        public bool AssociarGrupoTrabalho(Entidades.GrupoTrabalho grupoTrabalho)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;

                Entidades.NoticiaGrupoTrabalho noticiaGrupoTrabalho = new Entidades.NoticiaGrupoTrabalho();
                noticiaGrupoTrabalho.Noticia = Sessao.NoticiaAtual;
                noticiaGrupoTrabalho.GrupoTrabalho = grupoTrabalho;

                strRetorno = dalNoticiaGrupoTrabalho.Inserir(noticiaGrupoTrabalho);
                int intResult = 0;

                if (int.TryParse(strRetorno, out intResult))
                {
                    Entidades.Historico historico = new Entidades.Historico();
                    historico.Noticia = Sessao.NoticiaAtual;
                    historico.Usuario = Sessao.UsuarioLogado;
                    historico.DataHora = DateTime.Now;
                    historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.GrupoVinculado };

                    strRetorno = dalHistorico.Inserir(historico);
                }

                return (int.TryParse(strRetorno, out intResult));
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
