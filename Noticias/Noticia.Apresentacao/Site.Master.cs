using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            Session["NomeUsuario"] = null;
            Response.Redirect("~/Account/Login.aspx");
        }
    }
}
