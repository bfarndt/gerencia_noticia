using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Noticia.Negocios
{
    public static class Sessao
    {
        public static Entidades.Usuario UsuarioLogado;
        public static Entidades.Noticia NoticiaAtual;
        public static List<Entidades.UsuarioPermissao> UsuarioPermissoes;

        public static Timer TempoSessao { get; set; }
        public static bool comSessao { get; set; }

        public static void IniciarSessao() 
        {
            Sessao.TempoSessao = new Timer() { Enabled = true, Interval = 1000 };
            Sessao.TempoSessao.Elapsed += TempoSessao_Elapsed;
        }

        static void TempoSessao_Elapsed(object sender, ElapsedEventArgs e)
        {
            Sessao.TempoSessao.Stop();
            Sessao.comSessao = false;
        }
    }
}
