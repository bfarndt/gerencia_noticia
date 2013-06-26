using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class NoticiaImagem
    {
        public string Chave
        {
            get
            {
                return this.Noticia.IdNoticia.Value.ToString() + ";" + this.Imagem.IdImagem.Value.ToString();
            }
            set
            {
                Chave = this.Noticia.IdNoticia.Value.ToString() + ";" + this.Imagem.IdImagem.Value.ToString();
            }
        }
        public Noticia Noticia { get; set; }
        public Imagem Imagem { get; set; }
    }
}
