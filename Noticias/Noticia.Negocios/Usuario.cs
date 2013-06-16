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

        public void EfetuarAcesso()
        {
            CarregarPermissoes();
        }

        public bool ValidarUsuario()
        {
            if (Sessao.UsuarioLogado != null)
            {
                List<Entidades.Usuario> usuarios = dalUsuario.Consultar(Sessao.UsuarioLogado);

                var found = (from f in usuarios
                             where f.Senha == Sessao.UsuarioLogado.Senha
                             select f);

                if (found.Count() > 0)
                {
                    Sessao.UsuarioLogado = found.First();

                    Sessao.TempoSessao.Start();
                    Sessao.comSessao = true;
                }

                return Sessao.comSessao;
            }

            else
                return false;
        }

        public void CarregarPermissoes()
        {
            if (ValidarUsuario())
            {
                Sessao.UsuarioPermissoes = dalUsuarioPermissao.Consultar(new Entidades.UsuarioPermissao() { Usuario = Sessao.UsuarioLogado });
            }
            else
                Sessao.UsuarioPermissoes = null;
        }

        public bool ComSessao()
        {
            return Sessao.comSessao;
        }

        public bool TemPermissao(Entidades.PermissaoEnum permissao)
        {
            if (Sessao.UsuarioPermissoes != null)
            {
                int Count = (from f in Sessao.UsuarioPermissoes
                             where f.Permissao.IdPermissao == (int)permissao
                             select f).Count();

                return Count > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
