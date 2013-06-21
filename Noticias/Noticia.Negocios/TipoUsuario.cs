using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class TipoUsuario
    {
        public List<Entidades.TipoUsuario> Listar()
        {
            try
            {
                return new AcessoDados.TipoUsuario().Consultar(new Entidades.TipoUsuario() { IdTipoUsuario = null });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
