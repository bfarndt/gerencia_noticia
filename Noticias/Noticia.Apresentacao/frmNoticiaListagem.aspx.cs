using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao.Noticia
{
    public partial class frmNoticiaListagem : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.AbrirModal("www.google.com.br", "300", "Teste");
        }
    }
}