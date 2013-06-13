using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class DiasTrabalhados
    {
        public Usuario Usuario { get; set; }
        public DiaSemana DiaSemana { get; set; }
    }
}
