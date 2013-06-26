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
        private int IdNoticia = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(new Negocios.Usuario().TenhoPermissao(Entidades.PermissaoEnum.Criar_Noticia)))
                {
                    Response.Redirect("~/Default.aspx?Acesso=sem");
                }

                this.CarregarCombos();
            }
            else
            {
                if (ViewState["IdNoticia"] != null)
                    this.IdNoticia = Convert.ToInt32(ViewState["IdNoticia"]);
                else
                    this.IdNoticia = 0;
            }
        }

        protected void btn_salvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Entidades.Noticia noticia = new Entidades.Noticia() { Titulo = this.txtTitulo.Text };
                if (!new Negocios.Diretor().CriarNoticia(noticia))
                {
                    this.tabelaGrupo.Visible = false;
                    ExibirMensagem(TipoMensagem.Erro, "Erro ao criar notícia, verifique as informações adicionadas.");
                }
                else
                {
                    //Aproveitando a referencia
                    ViewState["IdNoticia"] = noticia.IdNoticia;
                    this.tabelaGrupo.Visible = true;
                    this.btn_salvar.Visible = false;
                    ExibirMensagem(TipoMensagem.Sucesso, "Notícia criada!");
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
            this.tabelaGrupo.Visible = false;
        }

        protected void btnVoltar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");

        }

        protected void grvGrupo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    string cod = Convert.ToString(e.CommandArgument);
                    List<Entidades.GrupoTrabalho> gruposSelecionados = ViewState["grupos"] as List<Entidades.GrupoTrabalho>;
                    Entidades.GrupoTrabalho grupoSelecionado = new Entidades.GrupoTrabalho();
                    grupoSelecionado.IdGrupoTrabalho = Convert.ToInt32(cod);

                    Entidades.NoticiaGrupoTrabalho noticiaGrupoTrabalho = new Entidades.NoticiaGrupoTrabalho();
                    noticiaGrupoTrabalho.GrupoTrabalho = grupoSelecionado;
                    noticiaGrupoTrabalho.Noticia = new Entidades.Noticia() { IdNoticia = this.IdNoticia };
                    if (new Negocios.Diretor().AssociarGrupoTrabalhoParaNoticia(noticiaGrupoTrabalho.GrupoTrabalho, noticiaGrupoTrabalho.Noticia))
                    {
                        AtualizarGridGrupos(grupoSelecionado, true);
                        ExibirMensagem(TipoMensagem.Sucesso, "Grupo removido.");
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

        private void AtualizarGridGrupos(Entidades.GrupoTrabalho grupo, bool excluir)
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    if (excluir && grupo != null && grupo.IdGrupoTrabalho > 0)
                    {
                        List<Entidades.GrupoTrabalho> gridGrupos = ViewState["grupos"] as List<Entidades.GrupoTrabalho>;
                        var consulta = (from f in gridGrupos
                                        where f.IdGrupoTrabalho == grupo.IdGrupoTrabalho
                                        select f);

                        gridGrupos.Remove(consulta.First());
                        ViewState["grupos"] = gridGrupos;

                        this.grvGrupo.DataSource = gridGrupos;
                        this.grvGrupo.DataBind();
                    }
                    else if (grupo != null)
                    {
                        List<Entidades.GrupoTrabalho> gridGrupos = ViewState["grupos"] as List<Entidades.GrupoTrabalho>;
                        if (gridGrupos == null)
                            gridGrupos = new List<Entidades.GrupoTrabalho>();
                        gridGrupos.Add(grupo);

                        ViewState["grupos"] = gridGrupos;
                        this.grvGrupo.DataSource = gridGrupos;
                        this.grvGrupo.DataBind();
                    }
                }
                else
                {
                    ExibirMensagem(TipoMensagem.Informacao, "É necessário criar a notícia antes desta operação.");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void imgOK_Grupo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdNoticia > 0)
                {
                    Entidades.NoticiaGrupoTrabalho noticiaGrupoTrabalho = new Entidades.NoticiaGrupoTrabalho();
                    noticiaGrupoTrabalho.Noticia = new Entidades.Noticia() { IdNoticia = this.IdNoticia };
                    noticiaGrupoTrabalho.GrupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = Convert.ToInt32(ddlGrupo.SelectedValue), Descricao = ddlGrupo.SelectedItem.Text };
                    if (new Negocios.Diretor().AssociarGrupoTrabalhoParaNoticia(noticiaGrupoTrabalho.GrupoTrabalho, noticiaGrupoTrabalho.Noticia))
                    {
                        AtualizarGridGrupos(noticiaGrupoTrabalho.GrupoTrabalho, false);
                        ExibirMensagem(TipoMensagem.Sucesso, "Grupo adicionado com sucesso.");
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Alerta, "Grupo não adicionado.");
                    }
                }
                else
                {
                    ExibirMensagem(TipoMensagem.Alerta, "É necessário criar a notícia antes desta operação.");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvGrupo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void CarregarCombos()
        {
            try
            {
                this.ddlGrupo.Items.Clear();
                this.ddlGrupo.DataTextField = "Descricao";
                this.ddlGrupo.DataValueField = "IdGrupoTrabalho";
                ViewState["comboGrupo"] = new Negocios.GrupoTrabalho().Listar(new Entidades.GrupoTrabalho() { IdGrupoTrabalho = null });
                this.ddlGrupo.DataSource = ViewState["comboGrupo"] as List<Entidades.GrupoTrabalho>;
                this.ddlGrupo.DataBind();
                this.ddlGrupo.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlGrupo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }
    }
}