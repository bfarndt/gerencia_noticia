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
        Negocios.Usuario NegUsuario = new Usuario();

        public bool CriarNoticia()
        {
            try
            {
                if (ValidarCampos())
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
        }

        public bool ValidarCampos()
        {
            return Sessao.NoticiaAtual != null && !((string.IsNullOrWhiteSpace(Sessao.NoticiaAtual.Titulo) || string.IsNullOrWhiteSpace(Sessao.NoticiaAtual.Conteudo)));
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
        }

        public List<Entidades.Noticia> NoticiasParaEdicao()
        {
            if (NegUsuario.TemPermissao(Entidades.PermissaoEnum.Editar_Noticia))
            {
                List<Entidades.Noticia> noticiasEdicao = new List<Entidades.Noticia>();

                List<Entidades.StatusNoticia> statusConsulta = new List<Entidades.StatusNoticia>();
                statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Criada });
                statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.GrupoVinculado });
                statusConsulta.Add(new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.ImagensAssociadas });

                List<Entidades.Historico> historicos = dalHistorico.Consultar(null, statusConsulta);
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

        public bool SubmeterEdicao() 
        {
            try
            {
                if (ValidarCampos())
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
        }

    }
}
