using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmVisualizarNoticia : System.Web.UI.Page
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
                    this.CarregarGrids();
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

        private void CarregarGrids()
        {
            try
            {
                Entidades.Noticia noticia = new Negocios.Noticia().NoticiaPorId(this.IdNoticia);
                txtTitulo.Text = noticia.Titulo;
                txtConteudo.Text = noticia.Conteudo;


                grvImagem.DataSource = new Negocios.Imagem().ImagensSelecionadasDaNoticia(this.IdNoticia);
                grvImagem.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }

        }

        protected void grvImagem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowType == DataControlRowType.DataRow))
                {
                    Entidades.Imagem imagem = e.Row.DataItem as Entidades.Imagem;
                    Image foto = (Image)e.Row.Cells[1].FindControl("imgFoto");
                    foto.ImageUrl = string.Concat("~/Imagem.ashx?idImg=", imagem.IdImagem);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }
    }
}