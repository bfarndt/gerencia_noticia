using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmListarNoticiaParaSubmissao : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Submeter_Noticia)))
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
                this.grvNoticia.DataSource = new Negocios.Noticia().NoticiasParaSubmissao(noticia);
                this.grvNoticia.DataBind();
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        public string GetPostGrid()
        {
            PostBackOptions options = new PostBackOptions(btnPost);
            Page.ClientScript.RegisterForEventValidation(options);
            return Page.ClientScript.GetPostBackEventReference(options);
        }

        protected void grvNoticia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "SUBMETER")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);

                    if (new Negocios.Reporter().SubmeterNoticia(new Entidades.Noticia() { IdNoticia = cod }))
                    {
                        this.CarregarGrid();
                        ExibirMensagem(TipoMensagem.Sucesso, "Notícia submetida com sucesso.");
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Alerta, "Notícia não submetida.");
                    }

                }
                else if (e.CommandName.Trim().ToUpper() == "CANCELAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);

                    if (new Negocios.Reporter().CancelarSubmissao(new Entidades.Noticia() { IdNoticia = cod }))
                    {
                        this.CarregarGrid();
                        ExibirMensagem(TipoMensagem.Informacao, "Notícia voltada edição.");
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Alerta, "Notícia não voltada edição.");
                    }
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
            this.ExibirMensagem(TipoMensagem.Informacao, "Atenção: Dados atualizados.");
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
    }
}