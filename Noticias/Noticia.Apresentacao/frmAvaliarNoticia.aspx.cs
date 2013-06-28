using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmAvaliarNoticia : System.Web.UI.Page
    {
        private int IdNoticia = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdNoticia"] != null && Request.QueryString["IdNoticia"].ToString().Length > 0)
                {
                    ViewState["IdNoticia"] = Convert.ToInt32(Request.QueryString["IdNoticia"]);
                    this.IdNoticia = Convert.ToInt32(Convert.ToInt32(ViewState["IdNoticia"]));
                    this.CarregarNoticia();
                }
            }
            else
            {
                if (ViewState["IdNoticia"] != null)
                {
                    this.IdNoticia = Convert.ToInt32(ViewState["IdNoticia"]);
                }
                else
                    this.IdNoticia = 0;
            }
        }

        private void CarregarNoticia()
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    Entidades.Noticia noticia = new Negocios.Noticia().NoticiaPorId(this.IdNoticia);
                    txtTitulo.Text = noticia.Titulo;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Notícia inválida.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void ibtCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    if (new Negocios.Editor().ReprovarNoticia(new Entidades.Noticia() { IdNoticia = this.IdNoticia }, txtFeedback.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "window.parent.post(); window.parent.hs.close();", true);
                        return;
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Notícia não reprovada.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Notícia inválida.');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void ibtEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    if (new Negocios.Editor().AprovarNoticia(new Entidades.Noticia() { IdNoticia = this.IdNoticia }, txtFeedback.Text))
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "window.parent.post(); window.parent.hs.close();", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Notícia não aprovada.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Notícia inválida.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }
    }
}