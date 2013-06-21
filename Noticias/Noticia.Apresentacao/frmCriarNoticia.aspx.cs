using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmCriarNoticia : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_salvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!new Negocios.Diretor().CriarNoticia(new Entidades.Noticia() { Titulo = this.txtTitulo.Text }))
                {
                    ExibirMensagem(TipoMensagem.Erro, "Erro ao criar notícia, verifique as informações adicionadas.");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void btnNovo_Click(object sender, ImageClickEventArgs e)
        {
            this.txtTitulo.Text = string.Empty;
        }

        protected void btnVoltar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");

        }



    }
}