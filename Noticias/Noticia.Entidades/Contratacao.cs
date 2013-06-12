using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Contratacao
    {
        public Usuario Usuario { get; set; }
        public DateTime? DataHora { get; set; }
    }
}
