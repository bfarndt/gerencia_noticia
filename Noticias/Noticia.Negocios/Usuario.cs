using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Noticia.Negocios
{
    public class Usuario
    {
        AcessoDados.Usuario dalUsuario = new AcessoDados.Usuario();
        AcessoDados.UsuarioPermissao dalUsuarioPermissao = new AcessoDados.UsuarioPermissao();

        public Usuario()
        {

        }

        public bool Logar()
        {
            try
            {
                if (Singleton.UsuarioLogado != null)
                {
                    List<Entidades.Usuario> usuarios = dalUsuario.Consultar(Singleton.UsuarioLogado);

                    var found = (from f in usuarios
                                 where f.Senha == Singleton.UsuarioLogado.Senha
                                 select f);

                    if (found.Count() > 0)
                    {
                        Singleton.IniciarSessao();
                        Singleton.UsuarioLogado = found.First();
                        CarregarPermissoes();
                        Singleton.TempoSessao.Start();
                        Singleton.comSessao = true;
                    }

                    return Singleton.comSessao;
                }

                else
                    return false;
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

        public void CarregarPermissoes()
        {

            try
            {
                Singleton.UsuarioPermissoes = dalUsuarioPermissao.Consultar(new Entidades.UsuarioPermissao() { Usuario = Singleton.UsuarioLogado });
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

        public bool TenhoPermissao(Entidades.PermissaoEnum permissao)
        {
            if (Singleton.UsuarioPermissoes != null)
            {
                int Count = (from f in Singleton.UsuarioPermissoes
                             where f.Permissao.IdPermissao == (int)permissao
                             select f).Count();

                return Count > 0;
            }
            else
            {
                return false;
            }
        }

        public bool TemNomeELogin(Entidades.Usuario usuario)
        {
            return !((string.IsNullOrWhiteSpace(usuario.Nome) || (string.IsNullOrWhiteSpace(usuario.Login))));
        }

        public bool TemNomeExistente(Entidades.Usuario usuario)
        {
            var usuariosAproximados = dalUsuario.Consultar(usuario);
            if (usuariosAproximados.Count > 0)
            {
                int found = (from f in usuariosAproximados
                             where f.Nome == usuario.Nome
                             select f).Count();
                return (found > 0);
            }
            else
            {
                return true;
            }
        }

        public List<Entidades.Usuario> Listar(Entidades.Usuario usuario)
        {
            try
            {
                return dalUsuario.Consultar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
