using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class _Default : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Acesso"] != null && Request.QueryString["Acesso"].ToString().Length > 0)
                {
                    ExibirMensagem(TipoMensagem.Alerta, "Sem acesso a esta tela.");
                }
            }
        }
    }
}
