using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Noticia.Negocios
{
    public class Usuario
    {
        public Entidades.Usuario usuarioLogado { get; set; }
        private Timer TempoSessao;
        private bool comSessao = false;

        public Usuario()
        {
            this.TempoSessao = new Timer() { Enabled = true, Interval = 1000 };
            this.TempoSessao.Elapsed += TempoSessao_Elapsed;
        }

        void TempoSessao_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.TempoSessao.Stop();
            this.comSessao = false;
        }

        public string EfetuarAcesso()
        {
            ComSessao();
            ValidarUsuario();
            Permissoes();

            return string.Empty;
        }

        public bool ValidarUsuario()
        {
            if (this.usuarioLogado != null)
            {
                this.TempoSessao.Start();
                this.comSessao = true;
                return this.comSessao;
            }
            else
                return false;
        }

        public List<Entidades.Permissao> Permissoes()
        {
            if (ValidarUsuario())
                return new List<Entidades.Permissao>();
            else
                return null;
        }

        public bool ComSessao()
        {
            return this.comSessao;
        }
    }
}
