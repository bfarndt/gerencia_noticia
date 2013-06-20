using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Noticia
    {
        public int? IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public List<PalavraChave> PalavrasChave { get; set; }
        public StatusNoticia StatusNoticia { get; set; }
    }
}