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
                Menu mnuMenu = ((Master.FindControl("NavigationMenu") as Menu)) as Menu;
                mnuMenu.Items.Clear();

                if (Negocios.Singleton.UsuarioLogado != null && !Negocios.Singleton.comSessao)
                    ExibirMensagem(TipoMensagem.Informacao, "Sessão expirada.");
            }
            else
            {

            }
        }

        protected void LoginButton_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.UserName.Text) && (!string.IsNullOrWhiteSpace(this.Password.Text)))
            {
                Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { Login = this.UserName.Text, Senha = this.Password.Text };
                bool sucesso = new Negocios.Usuario().Logar();

                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Efetuar_Acesso)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

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
    }

}
