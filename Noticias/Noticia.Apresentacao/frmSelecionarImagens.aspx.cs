using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmSelecionarImagens : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregarGrid();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            this.CarregarGrid();
            this.ExibirMensagem(TipoMensagem.Informacao, "Atenção: Dados atualizados.");
        }

        private void CarregarGrid()
        {
            try
            {
                this.grvNoticiaImagem.DataSource = new Negocios.Noticia().NoticiasImagensAssociadas();
                this.grvNoticiaImagem.DataBind();
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

        protected void grvNoticiaImagem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "SELECIONAR")
                {
                    string[] chave = e.CommandArgument.ToString().Split(';');
                    base.AbrirModal(Page.ResolveClientUrl("frmGerenciarSelecionarImagem.aspx?IdImagem=" + chave[1]), "800", "Selecionar Imagem","400");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvNoticiaImagem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}