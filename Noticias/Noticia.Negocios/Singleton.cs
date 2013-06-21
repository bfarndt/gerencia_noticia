using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Noticia.Negocios
{
    public static class Singleton
    {
        public static Entidades.Usuario UsuarioLogado;
        public static List<Entidades.UsuarioPermissao> UsuarioPermissoes;

        public static Timer TempoSessao { get; set; }
        public static bool comSessao = false;

        public static void IniciarSessao()
        {
            Singleton.TempoSessao = new Timer() { Enabled = true, Interval = 1000 };
            Singleton.TempoSessao.Elapsed += TempoSessao_Elapsed;
        }

        static void TempoSessao_Elapsed(object sender, ElapsedEventArgs e)
        {
            Singleton.TempoSessao.Stop();
            Singleton.comSessao = false;
        }

        public enum CRUDEnum
        {
            INSERIR = 1,
            ALTERAR = 2,
            DELETAR = 3
        }
    }


}