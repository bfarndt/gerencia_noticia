using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmEditarNoticia : System.Web.UI.Page
    {
        private int IdNoticia = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Editar_Noticia)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

                if (Request.QueryString["IdNoticia"] != null && Request.QueryString["IdNoticia"].ToString().Length > 0)
                {
                    ViewState["IdNoticia"] = Convert.ToInt32(Request.QueryString["IdNoticia"]);
                    this.IdNoticia = Convert.ToInt32(Convert.ToInt32(ViewState["IdNoticia"]));
                    this.CarregarNoticia();
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

        private void CarregarNoticia()
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    var found = from f in new Negocios.Noticia().NoticiasParaEdicao(null)
                                where f.IdNoticia == this.IdNoticia
                                select f;
                    if (found.Count() > 0)
                    {
                        txtTitulo.Text = found.First().Titulo;
                        txtConteudo.Text = found.First().Conteudo;
                    }

                    AtualizarGridPalavras(null, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        private void AtualizarGridPalavras(Entidades.PalavraChave palavra, bool excluir)
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    if (excluir && palavra != null && !string.IsNullOrWhiteSpace(palavra.PalavraChaveTexto))
                    {
                        List<Entidades.PalavraChave> gridPalavras = ViewState["palavras"] as List<Entidades.PalavraChave>;
                        var consulta = (from f in gridPalavras
                                        where f.PalavraChaveTexto == palavra.PalavraChaveTexto
                                        select f);

                        gridPalavras.Remove(consulta.First());
                        ViewState["palavras"] = gridPalavras;

                        this.grvPalavras.DataSource = gridPalavras;
                        this.grvPalavras.DataBind();
                    }
                    else if (palavra != null)
                    {
                        List<Entidades.PalavraChave> gridPalavras = ViewState["palavras"] as List<Entidades.PalavraChave>;
                        if (gridPalavras == null)
                            gridPalavras = new List<Entidades.PalavraChave>();
                        gridPalavras.Add(palavra);

                        ViewState["palavras"] = gridPalavras;
                        this.grvPalavras.DataSource = gridPalavras;
                        this.grvPalavras.DataBind();
                    }
                    else
                    {
                        ViewState["palavras"] = new Negocios.Noticia().PalavrasChaveDaNoticia(new Entidades.Noticia() { IdNoticia = this.IdNoticia });
                        this.grvPalavras.DataSource = ViewState["palavras"] as List<Entidades.PalavraChave>;
                        this.grvPalavras.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('É necessário salvar a notícia antes desta operação.');", true);
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
                Entidades.Noticia noticia = new Entidades.Noticia();
                noticia.IdNoticia = this.IdNoticia;
                noticia.Titulo = txtTitulo.Text;
                noticia.Conteudo = txtConteudo.Text;

                if (ViewState["palavras"] != null)
                {
                    noticia.PalavrasChave = ViewState["palavras"] as List<Entidades.PalavraChave>;
                }

                if (new Negocios.Reporter().EditarNoticia(noticia))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "window.parent.post(); window.parent.hs.close();", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Não foi possível alterar.');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void imgOK_palavras_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    Entidades.PalavraChave palavra = new Entidades.PalavraChave();
                    palavra.PalavraChaveTexto = txtPalavra.Text;
                    txtPalavra.Text = string.Empty;
                    AtualizarGridPalavras(palavra, false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('É necessário salvar a notícia antes desta operação.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grvPalavras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    string palavra = Convert.ToString(e.CommandArgument);
                    List<Entidades.PalavraChave> PalavraChaveSelecionadas = ViewState["palavras"] as List<Entidades.PalavraChave>;
                    Entidades.PalavraChave PalavraChaveSelecionada = new Entidades.PalavraChave();
                    PalavraChaveSelecionada.PalavraChaveTexto = palavra;
                    AtualizarGridPalavras(PalavraChaveSelecionada, true);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Palavra chave removida.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grvPalavras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}