using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmManterUsuario : NoticiaPage
    {
        private int IdUsuario = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregarCombos();

                if (Request.QueryString["IdUsuario"] != null && Request.QueryString["IdUsuario"].ToString().Length > 0)
                {
                    ViewState["IdUsuario"] = Convert.ToInt32(Request.QueryString["IdUsuario"]);
                    this.IdUsuario = Convert.ToInt32(Convert.ToInt32(ViewState["IdUsuario"]));
                    this.CarregarUsuario();
                }
            }
            else
            {
                if (ViewState["IdUsuario"] != null)
                {
                    this.IdUsuario = Convert.ToInt32(ViewState["IdUsuario"]);

                    //if (ViewState["permissoes"] != null)
                    //{
                    //    grvPermissoes.DataSource = ViewState["permissoes"] as List<Entidades.Permissao>;
                    //    grvPermissoes.DataBind();
                    //}
                }
                else
                    this.IdUsuario = 0;


            }
        }

        protected void btn_salvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Entidades.Usuario usuario = new Entidades.Usuario();
                //Inserir
                PreencherUsuario(usuario);
                if (this.IdUsuario == 0)
                {
                    if (!(new Negocios.Diretor().ManterUsuario(usuario, Negocios.Singleton.CRUDEnum.INSERIR)))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Não foi possível completar a operação.');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "window.parent.post(); window.parent.hs.close();", true);
                        return;
                    }
                }
                else
                {
                    if (!(new Negocios.Diretor().ManterUsuario(usuario, Negocios.Singleton.CRUDEnum.ALTERAR)))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Não foi possível completar a operação.');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "window.parent.post(); window.parent.hs.close();", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        private void CarregarUsuario()
        {
            try
            {
                List<Entidades.Usuario> consulta = new Negocios.Usuario().Listar(new Entidades.Usuario() { IdUsuario = this.IdUsuario });
                if (consulta.Count > 0)
                {
                    ddlTipo.SelectedValue = consulta.First().TipoUsuario.IdTipoUsuario.ToString();
                    txtLogin.Text = consulta.First().Login;
                    txtSenha.Text = consulta.First().Senha;
                    txtNome.Text = consulta.First().Nome;
                    if (consulta.First().UsuarioEndereco != null)
                    {
                        txtEmail.Text = consulta.First().UsuarioEndereco.Email;
                        txtTelefone.Text = consulta.First().UsuarioEndereco.Telefone;
                    }

                    this.AtualizarGrid(null, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        private void PreencherUsuario(Entidades.Usuario usuario)
        {
            usuario.IdUsuario = this.IdUsuario;
            usuario.TipoUsuario = new Entidades.TipoUsuario() { IdTipoUsuario = Convert.ToInt32(this.ddlTipo.SelectedValue), Descricao = this.ddlTipo.Text };
            usuario.Login = txtLogin.Text;
            usuario.Senha = txtSenha.Text;
            usuario.Nome = txtNome.Text;
            usuario.UsuarioEndereco = new Entidades.UsuarioEndereco() { Email = txtEmail.Text, Telefone = txtTelefone.Text };
        }

        protected void btnNovo_Click(object sender, ImageClickEventArgs e)
        {
            ddlTipo.SelectedIndex = 0;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            this.IdUsuario = 0;

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
                    usuarioPermissao.Usuario = new Entidades.Usuario() { IdUsuario = this.IdUsuario };
                    if (new Negocios.Diretor().RemoverPermissaoDoUsuario(usuarioPermissao))
                    {
                        AtualizarGrid(permissaoSelecionada, true);
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

        private void AtualizarGrid(Entidades.Permissao permissao, bool excluir)
        {
            try
            {
                if (this.IdUsuario > 0)
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
                        ViewState["permissoes"] = new Negocios.Permissao().PermissoesPorUsuario(new Entidades.Usuario() { IdUsuario = this.IdUsuario });
                        this.grvPermissoes.DataSource = ViewState["permissoes"] as List<Entidades.Permissao>;
                        this.grvPermissoes.DataBind();
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

        protected void imgOK_permissao_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdUsuario > 0)
                {
                    Entidades.UsuarioPermissao usuarioPermissao = new Entidades.UsuarioPermissao();
                    usuarioPermissao.Usuario = new Entidades.Usuario() { IdUsuario = this.IdUsuario };
                    usuarioPermissao.Permissao = new Entidades.Permissao() { IdPermissao = Convert.ToInt32(ddlPermissao.SelectedValue), Descricao = ddlPermissao.SelectedItem.Text };
                    if (new Negocios.Diretor().AssociarPermissaoParaUsuario(usuarioPermissao))
                    {
                        AtualizarGrid(usuarioPermissao.Permissao, false);
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