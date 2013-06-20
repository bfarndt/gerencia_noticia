using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class PalavraChave
    {
        public int? IdPalavraChave { get; set; }
        public Noticia Noticia { get; set; }
        public string PalavraChaveTexto { get; set; }
    }
}
