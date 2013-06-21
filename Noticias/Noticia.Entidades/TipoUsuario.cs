using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class TipoUsuario
    {
        public int? IdTipoUsuario { get; set; }
        public string Descricao { get; set; }
    }

    public enum TipoUsuarioEnum
    {
        Diretor = 1,
        Editor = 2,
        Reporter = 3,
        Fotografo = 4
    }
}
