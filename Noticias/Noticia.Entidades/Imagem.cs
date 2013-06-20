using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Imagem
    {
        public int? IdImagem { get; set; }
        public string Legenda { get; set; }
        public bool Selecionada { get; set; }
        public ImagemGravacao ImagemGravacao { get; set; }
    }
}
