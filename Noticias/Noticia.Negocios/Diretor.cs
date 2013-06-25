using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Diretor : Usuario
    {
        AcessoDados.Usuario dalUsuario = new AcessoDados.Usuario();
        AcessoDados.Noticia dalNoticia = new AcessoDados.Noticia();
        AcessoDados.Historico dalHistorico = new AcessoDados.Historico();
        AcessoDados.NoticiaGrupoTrabalho dalNoticiaGrupoTrabalho = new AcessoDados.NoticiaGrupoTrabalho();
        AcessoDados.GrupoTrabalhoUsuario dalGrupoTrabalhoUsuario = new AcessoDados.GrupoTrabalhoUsuario();
        AcessoDados.UsuarioPermissao dalUsuarioPermissao = new AcessoDados.UsuarioPermissao();
        AcessoDados.UsuarioEndereco dalUsuarioEndereco = new AcessoDados.UsuarioEndereco();
        AcessoDados.Contratacao dalContratacao = new AcessoDados.Contratacao();
        AcessoDados.Trabalho dalTrabalho = new AcessoDados.Trabalho();
        AcessoDados.DiasTrabalhados dalDiasTrabalhados = new AcessoDados.DiasTrabalhados();
        AcessoDados.GrupoTrabalho dalGrupoTrabalho = new AcessoDados.GrupoTrabalho();
        Negocios.GrupoTrabalho NegGrupoTrabalho = new GrupoTrabalho();
        Negocios.Noticia NegNoticia = new Noticia();

        public bool CriarNoticia(Entidades.Noticia noticia)
        {
            try
            {
                if (NegNoticia.TemTitulo(noticia))
                {
                    //Executar insert
                    string strRetorno = string.Empty;

                    strRetorno = dalNoticia.Inserir(noticia);

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        noticia.IdNoticia = intResult;
                        Entidades.Historico historico = new Entidades.Historico();

                        historico.Noticia = noticia;
                        historico.Usuario = Singleton.UsuarioLogado;
                        historico.DataHora = DateTime.Now;
                        historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.Criada };

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

        public bool AssociarGrupoTrabalhoParaNoticia(Entidades.GrupoTrabalho grupoTrabalho, Entidades.Noticia noticia)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;

                Entidades.NoticiaGrupoTrabalho noticiaGrupoTrabalho = new Entidades.NoticiaGrupoTrabalho();
                noticiaGrupoTrabalho.Noticia = noticia;
                noticiaGrupoTrabalho.GrupoTrabalho = grupoTrabalho;

                strRetorno = dalNoticiaGrupoTrabalho.Inserir(noticiaGrupoTrabalho);
                int intResult = 0;

                if (int.TryParse(strRetorno, out intResult))
                {
                    Entidades.Historico historico = new Entidades.Historico();
                    historico.Noticia = noticia;
                    historico.Usuario = Singleton.UsuarioLogado;
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

        public bool AssociarGrupoTrabalhoParaUsuario(Entidades.GrupoTrabalhoUsuario grupoTrabalhoUsuario)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;

                if (NegGrupoTrabalho.TemGrupoTrabalhoExistente(grupoTrabalhoUsuario.GrupoTrabalho))
                {
                    strRetorno = dalGrupoTrabalhoUsuario.Inserir(grupoTrabalhoUsuario);
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

        public bool RemoverGrupoTrabalhoDoUsuario(Entidades.GrupoTrabalhoUsuario grupoTrabalhoUsuario)
        {
            try
            {
                string strRetorno = string.Empty;

                strRetorno = dalGrupoTrabalhoUsuario.Excluir(grupoTrabalhoUsuario);
                int intResult = 0;
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

        public bool ManterUsuario(Entidades.Usuario usuario, Singleton.CRUDEnum acao)
        {

            try
            {
                if (!(acao == Singleton.CRUDEnum.INSERIR) || (TemNomeExistente(usuario)) && TemNomeELogin(usuario))
                {
                    string strRetorno = string.Empty;
                    int intResult = 0;

                    switch (acao)
                    {
                        case Singleton.CRUDEnum.INSERIR:
                            strRetorno = dalUsuario.Inserir(usuario);
                            if (int.TryParse(strRetorno, out intResult))
                            {
                                usuario.IdUsuario = intResult;
                                usuario.UsuarioEndereco.Usuario = usuario;
                                strRetorno = dalUsuarioEndereco.Inserir(usuario.UsuarioEndereco);

                                usuario.Contratacao = new Entidades.Contratacao() { DataHora = DateTime.Now };
                                usuario.Contratacao.Usuario = usuario;
                                strRetorno = dalContratacao.Inserir(usuario.Contratacao);
                            }

                            break;
                        case Singleton.CRUDEnum.ALTERAR:

                            strRetorno = dalUsuario.Alterar(usuario);
                            usuario.UsuarioEndereco.Usuario = usuario;

                            if (dalUsuarioEndereco.Consultar(usuario.UsuarioEndereco).Count > 0)
                            {
                                strRetorno = dalUsuarioEndereco.Alterar(usuario.UsuarioEndereco);
                            }
                            else 
                            {
                                strRetorno = dalUsuarioEndereco.Inserir(usuario.UsuarioEndereco);
                            }
                            break;
                        case Singleton.CRUDEnum.DELETAR:

                            List<Entidades.Usuario> usuarios = dalUsuario.Consultar(usuario);
                            if (usuarios.Count > 0)
                                usuario = usuarios.First();

                            if (usuario.UsuarioEndereco != null)
                            {
                                usuario.UsuarioEndereco.Usuario = usuario;
                                strRetorno = dalUsuarioEndereco.Excluir(usuario.UsuarioEndereco);
                            }

                            if (usuario.Contratacao != null)
                            {
                                usuario.Contratacao.Usuario = usuario;
                                strRetorno = dalContratacao.Excluir(usuario.Contratacao);
                            }

                            strRetorno = dalUsuarioPermissao.ExcluirPorUsuario(usuario);
                            strRetorno = dalGrupoTrabalhoUsuario.ExcluirPorUsuario(usuario);

                            //De Seg a Dom
                            int[] dias = { 1, 2, 3, 4, 5, 6, 7 };

                            foreach (int item in dias)
                            {
                                strRetorno = dalDiasTrabalhados.Excluir(new Entidades.DiasTrabalhados() { Usuario = usuario, DiaSemana = new Entidades.DiaSemana() { IdDia = item } });
                            }

                            strRetorno = dalUsuario.Excluir(usuario);
                            break;
                        default:
                            strRetorno = "AÇÃO INEXISTENTE";
                            break;
                    }

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

        public bool AssociarPermissaoParaUsuario(Entidades.UsuarioPermissao usuarioPermissao)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;
                strRetorno = dalUsuarioPermissao.Inserir(usuarioPermissao);
                int intResult = 0;
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

        public bool AssociarPermissaoParaTipoUsuario(Entidades.TipoUsuario tipoUsuario, Entidades.Permissao permissao)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;
                Entidades.UsuarioPermissao usuarioPermissao;
                int intResult = 0;
                foreach (var usuario in dalUsuario.Consultar(new Entidades.Usuario() { TipoUsuario = tipoUsuario }))
                {
                    usuarioPermissao = new Entidades.UsuarioPermissao();
                    usuarioPermissao.Usuario = usuario;
                    usuarioPermissao.Permissao = permissao;

                    strRetorno = dalUsuarioPermissao.Inserir(usuarioPermissao);
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

        public bool RemoverPermissaoDoUsuario(Entidades.UsuarioPermissao usuarioPermissao)
        {
            try
            {
                string strRetorno = string.Empty;
                strRetorno = dalUsuarioPermissao.Excluir(usuarioPermissao);
                int intResult = 0;
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

        public bool ManterTrabalho(Entidades.Trabalho trabalho, Singleton.CRUDEnum acao)
        {
            try
            {

                string strRetorno = string.Empty;
                int intResult = 0;
                switch (acao)
                {
                    case Singleton.CRUDEnum.INSERIR:
                        strRetorno = dalTrabalho.Inserir(trabalho);
                        int.TryParse(strRetorno, out intResult);
                        trabalho.IdTrabalho = intResult;
                        break;
                    case Singleton.CRUDEnum.ALTERAR:
                        strRetorno = dalTrabalho.Alterar(trabalho);
                        break;
                    case Singleton.CRUDEnum.DELETAR:
                        strRetorno = dalTrabalho.Excluir(trabalho);
                        break;
                    default:
                        break;
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

        public bool DefinirDiaTrabalhado(Entidades.DiasTrabalhados diasTrabalhados)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;
                int intResult = 0;
                strRetorno = dalDiasTrabalhados.Inserir(diasTrabalhados);

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

        public bool RemoverDiaTrabalhado(Entidades.DiasTrabalhados diasTrabalhados)
        {
            try
            {
                string strRetorno = string.Empty;
                int intResult = 0;
                strRetorno = dalDiasTrabalhados.Excluir(diasTrabalhados);

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

        public bool ManterGrupoTrabalho(Entidades.GrupoTrabalho grupoTrabalho, Singleton.CRUDEnum acao)
        {
            try
            {
                if (!NegGrupoTrabalho.TemGrupoTrabalhoEmBranco(grupoTrabalho) && !NegGrupoTrabalho.TemGrupoTrabalhoExistente(grupoTrabalho))
                {
                    string strRetorno = string.Empty;

                    switch (acao)
                    {
                        case Singleton.CRUDEnum.INSERIR:
                            strRetorno = dalGrupoTrabalho.Inserir(grupoTrabalho);
                            break;
                        case Singleton.CRUDEnum.ALTERAR:
                            strRetorno = dalGrupoTrabalho.Alterar(grupoTrabalho);
                            break;
                        case Singleton.CRUDEnum.DELETAR:
                            strRetorno = dalGrupoTrabalho.Excluir(grupoTrabalho);
                            break;
                        default:
                            strRetorno = "AÇÃO INEXISTENTE";
                            break;
                    }
                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                        grupoTrabalho.IdGrupoTrabalho = intResult;

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


        public bool ManterNoticia(Entidades.Noticia noticia, Singleton.CRUDEnum acao)
        {
            try
            {
                string strRetorno = string.Empty;
                int intResult = 0;

                switch (acao)
                {
                    case Singleton.CRUDEnum.INSERIR:
                        strRetorno = dalNoticia.Inserir(noticia);
                        break;
                    case Singleton.CRUDEnum.ALTERAR:
                        strRetorno = dalNoticia.Alterar(noticia);
                        break;
                    case Singleton.CRUDEnum.DELETAR:
                        strRetorno = dalNoticia.Excluir(noticia);
                        break;
                    default:
                        strRetorno = "AÇÃO INEXISTENTE";
                        break;
                }
                if (int.TryParse(strRetorno, out intResult))
                    noticia.IdNoticia = intResult;

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
