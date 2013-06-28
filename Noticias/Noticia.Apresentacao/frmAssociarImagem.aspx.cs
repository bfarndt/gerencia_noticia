using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmAssociarImagem : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Associar_Imagens)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

                this.CarregarCombos();
                AtualizarGridNoticiaImagem(null, false);
            }
        }

        private void CarregarCombos()
        {
            try
            {
                this.ddlNoticia.Items.Clear();
                this.ddlNoticia.DataTextField = "Titulo";
                this.ddlNoticia.DataValueField = "IdNoticia";
                this.ddlNoticia.DataSource = new Negocios.Noticia().NoticiasDoGrupoTrabalhoNaoSubmetidasNaoAprovadas();
                this.ddlNoticia.DataBind();
                this.ddlNoticia.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlNoticia.SelectedIndex = 0;

                this.ddlImagem.Items.Clear();
                this.ddlImagem.DataTextField = "Legenda";
                this.ddlImagem.DataValueField = "IdImagem";

                var dataSource = new List<Entidades.Imagem>();
                foreach (var item in new Negocios.Imagem().ImagensNaoSelecionadas())
                {
                    dataSource.Add(item.Imagem);
                }
                this.ddlImagem.DataSource = dataSource;
                this.ddlImagem.DataBind();
                this.ddlImagem.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlImagem.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void imgOK_Imagem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ddlNoticia.SelectedIndex > 0 && ddlImagem.SelectedIndex > 0)
                {
                    Entidades.NoticiaImagem noticiaImagem = new Entidades.NoticiaImagem();
                    noticiaImagem.Noticia = new Entidades.Noticia() { IdNoticia = Convert.ToInt32(ddlNoticia.SelectedValue), Titulo = ddlNoticia.SelectedItem.Text };
                    noticiaImagem.Imagem = new Entidades.Imagem() { IdImagem = Convert.ToInt32(ddlImagem.SelectedValue), Legenda = ddlImagem.SelectedItem.Text };
                    if (new Negocios.Fotografo().AssociarImagem(noticiaImagem.Noticia, noticiaImagem.Imagem))
                    {
                        AtualizarGridNoticiaImagem(noticiaImagem, false);
                        ExibirMensagem(TipoMensagem.Sucesso, "Noticia associada com sucesso.");
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Alerta, "Notícia não Associada.");
                    }
                }
                else
                {
                    ExibirMensagem(TipoMensagem.Alerta, "Selecione a notícia e a imagem");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvNoticiaImagem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    string[] strChave = e.CommandArgument.ToString().Split(';');
                    List<Entidades.NoticiaImagem> selecianadas = ViewState["noticiasImagens"] as List<Entidades.NoticiaImagem>;
                    Entidades.NoticiaImagem noticiaImagemSelecionado = new Entidades.NoticiaImagem();
                    noticiaImagemSelecionado.Noticia = new Entidades.Noticia() { IdNoticia = Convert.ToInt32(strChave[0]) };
                    noticiaImagemSelecionado.Imagem = new Entidades.Imagem() { IdImagem = Convert.ToInt32(strChave[1]) };

                    if (new Negocios.Fotografo().DesassociarImagem(noticiaImagemSelecionado.Noticia, noticiaImagemSelecionado.Imagem))
                    {
                        AtualizarGridNoticiaImagem(noticiaImagemSelecionado, true);
                        ExibirMensagem(TipoMensagem.Sucesso, "Associação removida.");
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Alerta, "Não foi possível completar a operação.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        private void AtualizarGridNoticiaImagem(Entidades.NoticiaImagem noticiaImagemSelecionado, bool excluir)
        {
            try
            {
                if ((excluir && noticiaImagemSelecionado != null && noticiaImagemSelecionado.Imagem != null && noticiaImagemSelecionado.Imagem.IdImagem > 0) &&
                    (excluir && noticiaImagemSelecionado != null && noticiaImagemSelecionado.Noticia != null && noticiaImagemSelecionado.Noticia.IdNoticia > 0)
                    )
                {
                    List<Entidades.NoticiaImagem> gridNoticiasImagens = ViewState["noticiasImagens"] as List<Entidades.NoticiaImagem>;
                    var consulta = (from f in gridNoticiasImagens
                                    where f.Noticia.IdNoticia == noticiaImagemSelecionado.Noticia.IdNoticia &&
                                          f.Imagem.IdImagem == noticiaImagemSelecionado.Imagem.IdImagem
                                    select f);

                    gridNoticiasImagens.Remove(consulta.First());
                    ViewState["noticiasImagens"] = gridNoticiasImagens;

                    this.grvNoticiaImagem.DataSource = gridNoticiasImagens;
                    this.grvNoticiaImagem.DataBind();
                }
                else if ((noticiaImagemSelecionado != null && noticiaImagemSelecionado.Imagem != null && noticiaImagemSelecionado.Imagem.IdImagem > 0) &&
                        (noticiaImagemSelecionado != null && noticiaImagemSelecionado.Noticia != null && noticiaImagemSelecionado.Noticia.IdNoticia > 0)
                    )
                {
                    List<Entidades.NoticiaImagem> gridNoticiasImagens = ViewState["noticiasImagens"] as List<Entidades.NoticiaImagem>;
                    if (gridNoticiasImagens == null)
                        gridNoticiasImagens = new List<Entidades.NoticiaImagem>();
                    gridNoticiasImagens.Add(noticiaImagemSelecionado);

                    ViewState["noticiasImagens"] = gridNoticiasImagens;
                    this.grvNoticiaImagem.DataSource = gridNoticiasImagens;
                    this.grvNoticiaImagem.DataBind();
                }
                else
                {
                    ViewState["noticiasImagens"] = new Negocios.Noticia().ImagensDeNoticiasAssociadas();
                    this.grvNoticiaImagem.DataSource = ViewState["noticiasImagens"] as List<Entidades.NoticiaImagem>;
                    this.grvNoticiaImagem.DataBind();
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