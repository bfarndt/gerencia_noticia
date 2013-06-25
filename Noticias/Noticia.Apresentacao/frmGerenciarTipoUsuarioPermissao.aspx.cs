using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmGerenciarTipoUsuarioPermissao : System.Web.UI.Page
    {
        private int IdTipoUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.CarregarCombos();

                    if (Request.QueryString["IdTipoUsuario"] != null && Request.QueryString["IdTipoUsuario"].ToString().Length > 0)
                    {
                        ViewState["IdTipoUsuario"] = Convert.ToInt32(Request.QueryString["IdTipoUsuario"]);
                        this.IdTipoUsuario = Convert.ToInt32(Convert.ToInt32(ViewState["IdTipoUsuario"]));
                        this.AtualizarGridPermissoes(null, false);
                    }
                }
                else
                {
                    if (ViewState["IdTipoUsuario"] != null)
                    {
                        this.IdTipoUsuario = Convert.ToInt32(ViewState["IdTipoUsuario"]);
                    }
                    else
                        this.IdTipoUsuario = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        private void CarregarCombos()
        {
            try
            {
                this.ddlPermissao.Items.Clear();
                this.ddlPermissao.DataTextField = "Descricao";
                this.ddlPermissao.DataValueField = "IdPermissao";
                ViewState["comboPermissao"] = new Negocios.Permissao().Listar();
                this.ddlPermissao.DataSource = ViewState["comboPermissao"] as List<Entidades.Permissao>;
                this.ddlPermissao.DataBind();
                this.ddlPermissao.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlPermissao.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grvPermissoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    string cod = Convert.ToString(e.CommandArgument);
                    List<Entidades.Permissao> PermissoesSelecionadas = ViewState["permissoes"] as List<Entidades.Permissao>;
                    Entidades.Permissao permissaoSelecionada = new Entidades.Permissao();
                    permissaoSelecionada.IdPermissao = Convert.ToInt32(cod);

                    Entidades.UsuarioPermissao usuarioPermissao = new Entidades.UsuarioPermissao();
                    usuarioPermissao.Permissao = permissaoSelecionada;
                    usuarioPermissao.Usuario = new Entidades.Usuario() { IdUsuario = this.IdTipoUsuario };
                    if (new Negocios.Diretor().RemoverPermissaoDoTipoUsuario(new Entidades.TipoUsuario() { IdTipoUsuario = this.IdTipoUsuario }, usuarioPermissao.Permissao))
                    {
                        AtualizarGridPermissoes(permissaoSelecionada, true);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Permissão removida.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Não foi possível completar a operação.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grvPermissoes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void AtualizarGridPermissoes(Entidades.Permissao permissao, bool excluir)
        {
            try
            {
                if (this.IdTipoUsuario > 0)
                {
                    if (excluir && permissao != null && permissao.IdPermissao > 0)
                    {
                        List<Entidades.Permissao> gridPermissoes = ViewState["permissoes"] as List<Entidades.Permissao>;
                        var consulta = (from f in gridPermissoes
                                        where f.IdPermissao == permissao.IdPermissao
                                        select f);

                        gridPermissoes.Remove(consulta.First());
                        ViewState["permissoes"] = gridPermissoes;

                        this.grvPermissoes.DataSource = gridPermissoes;
                        this.grvPermissoes.DataBind();
                    }
                    else if (permissao != null)
                    {
                        List<Entidades.Permissao> gridPermissoes = ViewState["permissoes"] as List<Entidades.Permissao>;
                        if (gridPermissoes == null)
                            gridPermissoes = new List<Entidades.Permissao>();
                        gridPermissoes.Add(permissao);

                        ViewState["permissoes"] = gridPermissoes;
                        this.grvPermissoes.DataSource = gridPermissoes;
                        this.grvPermissoes.DataBind();
                    }
                    else
                    {
                        ViewState["permissoes"] = new Negocios.Permissao().PermissoesPorTipoUsuario(new Entidades.TipoUsuario() { IdTipoUsuario = this.IdTipoUsuario });
                        this.grvPermissoes.DataSource = ViewState["permissoes"] as List<Entidades.Permissao>;
                        this.grvPermissoes.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('É necessário salvar o tipo usuário antes desta operação.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void imgOK_permissao_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdTipoUsuario > 0)
                {
                    Entidades.UsuarioPermissao usuarioPermissao = new Entidades.UsuarioPermissao();
                    usuarioPermissao.Permissao = new Entidades.Permissao() { IdPermissao = Convert.ToInt32(ddlPermissao.SelectedValue), Descricao = ddlPermissao.SelectedItem.Text };
                    if (new Negocios.Diretor().AssociarPermissaoParaTipoUsuario(new Entidades.TipoUsuario() { IdTipoUsuario = this.IdTipoUsuario }, usuarioPermissao.Permissao))
                    {
                        AtualizarGridPermissoes(usuarioPermissao.Permissao, false);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Permissão adicionada com sucesso.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Permissão não adicionada.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('É necessário salvar o usuário antes desta operação.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }
    }
}