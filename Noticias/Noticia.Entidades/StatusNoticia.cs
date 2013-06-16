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

    public enum StatusNoticiaEnum
    {
        Criada = 1,
        GrupoVinculado = 2,
        ImagensAssociadas = 3,
        Editada = 4,
        Submetida = 5,
        Aprovada = 6
    }
}
