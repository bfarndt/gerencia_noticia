using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmGerenciarSelecionarImagem : System.Web.UI.Page
    {
        private int IdImagem = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdImagem"] != null && Request.QueryString["IdImagem"].ToString().Length > 0)
                {
                    ViewState["IdImagem"] = Convert.ToInt32(Request.QueryString["IdImagem"]);
                    this.IdImagem = Convert.ToInt32(Convert.ToInt32(ViewState["IdImagem"]));
                    this.CarregarGravacao();
                }
            }
            else
            {
                if (ViewState["IdImagem"] != null)
                {
                    this.IdImagem = Convert.ToInt32(ViewState["IdImagem"]);
                }
                else
                    this.IdImagem = 0;


            }
        }

        private void CarregarGravacao()
        {
            try
            {
                Entidades.ImagemArquivo consulta = new Negocios.Imagem().CarregarImagemArquivo(new Entidades.Imagem() { IdImagem = this.IdImagem });

                if (this.IdImagem > 0 && consulta != null)
                {
                    txtLegenda.Text = consulta.Imagem.Legenda;
                    if (consulta.Imagem != null && consulta.Imagem.ImagemGravacao != null)
                    {
                        txtDataHora.Text = consulta.Imagem.ImagemGravacao.DataHoraGravacao.HasValue ? consulta.Imagem.ImagemGravacao.DataHoraGravacao.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        txtLocal.Text = consulta.Imagem.ImagemGravacao.LocalGravacao;
                    }
                    imgImagem.ImageUrl = string.Concat("~/Imagem.ashx?idImg=", this.IdImagem.ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Imagem não carregada.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btn_salvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DateTime data = DateTime.Now;
                DateTime.TryParse(txtDataHora.Text, out data);

                if (new Negocios.Reporter().SelecionarImagem(new Entidades.Imagem()
                {
                    IdImagem = this.IdImagem,
                    Legenda = txtLegenda.Text,
                    ImagemGravacao = new Entidades.ImagemGravacao()
                    {
                        LocalGravacao = txtLocal.Text,
                        DataHoraGravacao = data
                    }
                }))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "window.parent.post(); window.parent.hs.close();", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Erro ao gravar.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }
    }
}