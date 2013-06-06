using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noticia.Entidades
{
    [Serializable]
    public class UsuarioEndereco
    {
        public Usuario Usuario { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
