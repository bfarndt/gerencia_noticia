using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public UsuarioEndereco UsuarioEndereco { get; set; }
        public Contratacao Contratacao { get; set; }
    }
}