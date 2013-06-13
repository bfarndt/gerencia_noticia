using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class GrupoTrabalhoUsuario
    {
        public Usuario Usuario { get; set; }
        public GrupoTrabalho GrupoTrabalho { get; set; }
    }
}
