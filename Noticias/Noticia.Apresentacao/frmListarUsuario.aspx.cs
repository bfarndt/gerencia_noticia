using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmListarUsuario : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        protected void grvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "VISUALIZAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmManterUsuario.aspx?IdUsuario=" + string.Concat(cod.ToString())), "800", "Usuário Visualizar", "500");
                }
                if (e.CommandName.Trim().ToUpper() == "EDITAR")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);
                    base.AbrirModal(Page.ResolveClientUrl("frmManterUsuario.aspx?IdUsuario=" + string.Concat(cod.ToString())), "800", "Usuário Visualizar", "500");
                }
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    Entidades.Usuario usuario = new Entidades.Usuario();
                    usuario.IdUsuario = Convert.ToInt32(e.CommandArgument);
                    new Negocios.Diretor().ManterUsuario(usuario, Negocios.Singleton.CRUDEnum.DELETAR);
                    this.CarregarGrid();
                    this.ExibirMensagem(TipoMensagem.Sucesso, "Atenção: Usuário excluído com sucesso.");
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
            this.grvUsuario.EditIndex = -1;
            this.CarregarGrid();
        }

        protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        public void CarregarGrid()
        {
            try
            {
                Entidades.Usuario usuario = new Entidades.Usuario();
                if (ddlTipo.SelectedIndex > 0)
                    usuario.TipoUsuario = new Entidades.TipoUsuario() { IdTipoUsuario = Convert.ToInt32(ddlTipo.SelectedValue) };
                usuario.Nome = txtNome.Text;
                this.grvUsuario.DataSource = new Negocios.Usuario().Listar(usuario);
                this.grvUsuario.DataBind();
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }


        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            this.CarregarGrid();
            this.ExibirMensagem(TipoMensagem.Informacao, "Atenção: Dados atualizados.");
        }

        protected void btnNovo_Click(object sender, ImageClickEventArgs e)
        {
            this.AbrirModal(@"frmManterUsuario.aspx", "500", "Manter usuário");
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

        private void CarregarCombos()
        {
            try
            {
                this.ddlTipo.Items.Clear();
                this.ddlTipo.DataTextField = "Descricao";
                this.ddlTipo.DataValueField = "IdTipoUsuario";
                this.ddlTipo.DataSource = new Negocios.TipoUsuario().Listar();
                this.ddlTipo.DataBind();
                this.ddlTipo.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlTipo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }
    }
}