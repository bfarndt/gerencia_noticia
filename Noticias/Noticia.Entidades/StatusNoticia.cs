using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class StatusNoticia
    {
        public int? IdStatus { get; set; }
        public string Descricao { get; set; }
    }
}
