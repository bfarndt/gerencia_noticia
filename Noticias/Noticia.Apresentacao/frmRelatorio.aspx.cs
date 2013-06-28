using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmRelatorio : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Gerar_Relatorio)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

                this.CarregarCombos();
                this.CarregarGrid();
            }
        }

        public string GetPostGrid()
        {
            PostBackOptions options = new PostBackOptions(btnPost);
            Page.ClientScript.RegisterForEventValidation(options);
            return Page.ClientScript.GetPostBackEventReference(options);
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
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "VISUALIZAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmVisualizarNoticia.aspx?IdNoticia=" + string.Concat(cod.ToString())), "635", "Visualizar Notícia", "600");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void CarregarCombos()
        {
            try
            {
                this.ddlStatus.Items.Clear();
                this.ddlStatus.DataTextField = "Descricao";
                this.ddlStatus.DataValueField = "IdStatus";
                this.ddlStatus.DataSource = new Negocios.Noticia().TodosStatus();
                this.ddlStatus.DataBind();
                this.ddlStatus.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        public void CarregarGrid()
        {
            try
            {
                List<Entidades.StatusNoticia> consultaStatus = null;

                if (ddlStatus.SelectedIndex > 0)
                {
                    consultaStatus = new List<Entidades.StatusNoticia>();
                    consultaStatus.Add(new Entidades.StatusNoticia() { IdStatus = Convert.ToInt32(ddlStatus.SelectedValue) });
                }

                this.grvHistorico.DataSource = new Negocios.Noticia().GerarRelatorio(new Entidades.Noticia() { Titulo = txtTitulo.Text }, consultaStatus);
                this.grvHistorico.DataBind();
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {

        }


    }
}