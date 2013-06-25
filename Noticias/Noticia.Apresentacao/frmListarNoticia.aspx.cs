using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmListarNoticia : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregarGrid();
            }
        }

        public void CarregarGrid()
        {
            try
            {
                Entidades.Noticia noticia = new Entidades.Noticia();
                noticia.Titulo = txtTitulo.Text;
                this.grvNoticia.DataSource = new Negocios.Noticia().Listar(noticia);
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
                if (e.CommandName.Trim().ToUpper() == "VISUALIZAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmEditarNoticia.aspx?IdNoticia=" + string.Concat(cod.ToString())), "800", "Notícia Visualizar", "600");
                }
                if (e.CommandName.Trim().ToUpper() == "EDITAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmEditarNoticia.aspx?IdNoticia=" + string.Concat(cod.ToString())), "800", "Notícia Visualizar", "600");
                }
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    Entidades.Noticia Noticia = new Entidades.Noticia();
                    Noticia.IdNoticia = Convert.ToInt32(e.CommandArgument);
                    new Negocios.Diretor().ManterNoticia(Noticia, Negocios.Singleton.CRUDEnum.DELETAR);
                    this.CarregarGrid();
                    this.ExibirMensagem(TipoMensagem.Sucesso, "Atenção: Grupo Trabalho excluído com sucesso.");
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    if (ex.Message.Contains("FK_"))
                    {
                        this.ExibirMensagem(TipoMensagem.Erro, "Atenção: Não foi possível excluir o registro selecionado, pois o mesmo esta vinculado a outros registros do sistema");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            this.grvNoticia.EditIndex = -1;
            this.CarregarGrid();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            this.CarregarGrid();
            this.ExibirMensagem(TipoMensagem.Informacao, "Atenção: Dados atualizados.");
        }

        protected void grvNoticia_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        protected void btnNovo_Click(object sender, ImageClickEventArgs e)
        {
            this.AbrirModal(@"frmEditarNoticia.aspx?IdNoticia=0", "500", "Manter Noticia");
        }
    }
}