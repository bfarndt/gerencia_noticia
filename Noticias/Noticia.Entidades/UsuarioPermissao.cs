using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class UsuarioPermissao
    {
        public Usuario Usuario { get; set; }
        public Permissao Permissao { get; set; }
    }
}
