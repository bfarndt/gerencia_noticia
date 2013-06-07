using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class NoticiaImagem
    {
        public Noticia Noticia { get; set; }
        public Imagem Imagem { get; set; }
    }
}
