using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao.Account
{
    public partial class Login : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.UserName.Text) && (!string.IsNullOrWhiteSpace(this.Password.Text)))
            {
                Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { Login = this.UserName.Text, Senha = this.Password.Text };
                bool sucesso = new Negocios.Usuario().Logar();
                if (sucesso)
                {
                    Session["NomeUsuario"] = Negocios.Singleton.UsuarioLogado.Nome;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    Session["NomeUsuario"] = null;
                    this.ExibirMensagem(TipoMensagem.Informacao, "Usuário/Senha incorreta!");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.AbrirModal("www.google.com.br", "300", "Teste");
        }
    }

}
