using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Permissao
    {
        AcessoDados.Permissao dalPermissao = new AcessoDados.Permissao();
        AcessoDados.UsuarioPermissao dalUsuarioPermissao = new AcessoDados.UsuarioPermissao();
        AcessoDados.Usuario dalUsuario = new AcessoDados.Usuario();

        public List<Entidades.Permissao> Listar() 
        {
            try
            {
                return dalPermissao.Consultar(new Entidades.Permissao() { IdPermissao = null });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidades.Permissao> PermissoesPorUsuario(Entidades.Usuario usuario) 
        {
            try
            {
                List<Entidades.Permissao> retorno = new List<Entidades.Permissao>();

                foreach (var item in dalUsuarioPermissao.Consultar(new Entidades.UsuarioPermissao() { Usuario = usuario }))
                {
                    retorno.Add(item.Permissao);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidades.Permissao> PermissoesPorTipoUsuario(Entidades.TipoUsuario tipoUsuario)
        {
            try
            {
                List<Entidades.Permissao> retorno = new List<Entidades.Permissao>();
                List<Entidades.Usuario> usuarios = dalUsuario.Consultar(new Entidades.Usuario() { TipoUsuario = tipoUsuario });

                foreach (var usuario in usuarios)
                {
                    foreach (var permissao in dalUsuarioPermissao.Consultar(new Entidades.UsuarioPermissao() { Usuario = usuario }))
                    {
                        int found = (from f in retorno
                                    where f.IdPermissao == permissao.Permissao.IdPermissao
                                    select f).Count();
                        if (found > 0)
                            continue;
                        else
                            retorno.Add(permissao.Permissao);
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
