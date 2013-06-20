using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class ImagemGravacao
    {

        public Imagem Imagem { get; set; }
        public DateTime? DataHoraGravacao { get; set; }
        public String LocalGravacao { get; set; }
    }
}
