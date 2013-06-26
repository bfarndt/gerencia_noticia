using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmListarTipoUsuarioPermissao : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Manter_Usuario)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

                this.CarregarGridView();
            }
        }

        public string GetPostGrid()
        {
            PostBackOptions options = new PostBackOptions(btnPost);
            Page.ClientScript.RegisterForEventValidation(options);
            return Page.ClientScript.GetPostBackEventReference(options);
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            this.CarregarGridView();
            this.ExibirMensagem(TipoMensagem.Informacao, "Atenção: Dados atualizados.");
        }

        protected void grvTipoUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "PERMISSAO")
                {
                    int cod = Convert.ToInt32(e.CommandArgument);

                    var found = from f in new Negocios.TipoUsuario().Listar()
                                where f.IdTipoUsuario == cod
                                select f;
                    Entidades.TipoUsuario tipo = found.First();
                    base.AbrirModal(Page.ResolveClientUrl("frmGerenciarTipoUsuarioPermissao.aspx?IdTipoUsuario=" + string.Concat(cod.ToString())), "500", tipo.Descricao, "297");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvTipoUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void CarregarGridView()
        {
            try
            {
                this.grvTipoUsuario.DataSource = new Negocios.TipoUsuario().Listar();
                this.grvTipoUsuario.DataBind();
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
    }
}