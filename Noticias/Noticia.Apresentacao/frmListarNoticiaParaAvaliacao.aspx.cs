using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmListarNoticiaParaAvaliacao : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Avaliar_Noticia)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

                this.CarregarGrid();
            }
        }

        private void CarregarGrid()
        {
            try
            {
                Entidades.Noticia noticia = new Entidades.Noticia();
                noticia.Titulo = txtTitulo.Text;
                this.grvNoticia.DataSource = new Negocios.Noticia().NoticiasParaAvaliacao(noticia);
                this.grvNoticia.DataBind();
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void btnVoltar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.CarregarGrid();
            }
            catch (Exception ex)
            {
                this.ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvNoticia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "AVALIAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmAvaliarNoticia.aspx?IdNoticia=" + string.Concat(cod.ToString())), "620", "Avaliar Notícia", "500");
                }
                else if (e.CommandName.Trim().ToUpper() == "VISUALIZAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmVisualizarNoticia.aspx?IdNoticia=" + string.Concat(cod.ToString())), "635", "Visualizar Notícia", "600");
                }
            }
            catch (Exception ex)
            {
                this.ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
            this.grvNoticia.EditIndex = -1;
        }

        protected void grvNoticia_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            this.CarregarGrid();
            this.ExibirMensagem(TipoMensagem.Informacao, "Avaliação efetuada.");
        }

        public string GetPostGrid()
        {
            PostBackOptions options = new PostBackOptions(btnPost);
            Page.ClientScript.RegisterForEventValidation(options);
            return Page.ClientScript.GetPostBackEventReference(options);
        }
    }
}