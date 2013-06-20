using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class GrupoTrabalho
    {
        public int? IdGrupoTrabalho { get; set; }
        public string Descricao { get; set; }
    }
}
