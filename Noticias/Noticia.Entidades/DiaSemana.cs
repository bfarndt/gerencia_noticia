using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class DiaSemana
    {
        public int? IdDia { get; set; }
        public string Descricao { get; set; }
    }

    public enum DiaSemanaEnum
    {
        Domingo = 1,
        Segunda_feira = 2,
        Terca_feira = 3,
        Quarta_feira = 4,
        Quinta_feira = 5,
        Sexta_feira = 6,
        Sabado = 7
    }
}
