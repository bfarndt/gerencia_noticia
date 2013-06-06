using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Trabalho
    {
        public int? IdTrabalho { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public decimal? ValorHoraTrabalhada { get; set; }
    }
}
