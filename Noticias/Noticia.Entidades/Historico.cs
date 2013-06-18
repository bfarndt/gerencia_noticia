using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Historico
    {
        public int? IdHistorico { get; set; }
        public Noticia Noticia { get; set; }
        public Usuario Usuario { get; set; }
        public StatusNoticia StatusNoticia { get; set; }
        public DateTime? DataHora { get; set; }
        public string Descricao { get; set; }
    }
}
