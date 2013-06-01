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
            //if (!string.IsNullOrWhiteSpace(this.UserName.Text) && (!string.IsNullOrWhiteSpace(this.Password.Text))) 
            //{
            //    string NomeUsuario = new Noticia..Usuario().Validar(this.UserName.Text, this.Password.Text);
            //    if (NomeUsuario.ToLower() != "desconhecido")
            //    {
            //        Session["NomeUsuario"] = NomeUsuario;
            //        Response.Redirect("~/Cadastro/FuncionarioListar.aspx");
            //    }
            //    else
            //    {
            //        Session["NomeUsuario"] = null;
            //        this.ExibirMensagem(TipoMensagem.Informacao, "Usuário não encontrado!");
            //    }
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.AbrirModal("www.google.com.br", "300", "Teste");
        }
    }

}
