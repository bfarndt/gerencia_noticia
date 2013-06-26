using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmManterUsuario : System.Web.UI.Page
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
                ddlTipo_SelectedIndexChanged(null, null);
            }
            else
            {
                if (ViewState["IdUsuario"] != null)
                {
                    this.IdUsuario = Convert.ToInt32(ViewState["IdUsuario"]);
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

                    this.AtualizarGridPermissoes(null, false);
                    this.AtualizarGridGrupos(null, false);
                    this.AtualizarGridDias(null, false);
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

                this.ddlGrupo.Items.Clear();
                this.ddlGrupo.DataTextField = "Descricao";
                this.ddlGrupo.DataValueField = "IdGrupoTrabalho";
                ViewState["comboGrupo"] = new Negocios.GrupoTrabalho().Listar(new Entidades.GrupoTrabalho() { IdGrupoTrabalho = null });
                this.ddlGrupo.DataSource = ViewState["comboGrupo"] as List<Entidades.GrupoTrabalho>;
                this.ddlGrupo.DataBind();
                this.ddlGrupo.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlGrupo.SelectedIndex = 0;

                this.ddlDia.Items.Clear();
                this.ddlDia.DataTextField = "Descricao";
                this.ddlDia.DataValueField = "IdDia";
                ViewState["comboDias"] = new Negocios.Trabalho().ListarDiasSemana();
                this.ddlDia.DataSource = ViewState["comboDias"] as List<Entidades.DiaSemana>;
                this.ddlDia.DataBind();
                this.ddlDia.Items.Insert(0, new ListItem("Selecione", "0"));
                this.ddlDia.SelectedIndex = 0;
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
                        AtualizarGridPermissoes(permissaoSelecionada, true);
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


        private void AtualizarGridPermissoes(Entidades.Permissao permissao, bool excluir)
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
                        AtualizarGridPermissoes(usuarioPermissao.Permissao, false);
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

        protected void imgOK_Grupo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdUsuario > 0)
                {
                    Entidades.GrupoTrabalhoUsuario grupoTrabalhoUsuario = new Entidades.GrupoTrabalhoUsuario();
                    grupoTrabalhoUsuario.Usuario = new Entidades.Usuario() { IdUsuario = this.IdUsuario };
                    grupoTrabalhoUsuario.GrupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = Convert.ToInt32(ddlGrupo.SelectedValue), Descricao = ddlGrupo.SelectedItem.Text };
                    if (new Negocios.Diretor().AssociarGrupoTrabalhoParaUsuario(grupoTrabalhoUsuario))
                    {
                        AtualizarGridGrupos(grupoTrabalhoUsuario.GrupoTrabalho, false);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Grupo adicionado com sucesso.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Grupo não adicionado.');", true);
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

        private void AtualizarGridGrupos(Entidades.GrupoTrabalho grupo, bool excluir)
        {
            try
            {
                if (this.IdUsuario > 0)
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
                    else
                    {
                        ViewState["grupos"] = new Negocios.GrupoTrabalho().GruposPorUsuario(new Entidades.Usuario() { IdUsuario = this.IdUsuario });
                        this.grvGrupo.DataSource = ViewState["grupos"] as List<Entidades.GrupoTrabalho>;
                        this.grvGrupo.DataBind();
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

                    Entidades.GrupoTrabalhoUsuario grupoTrabalhoUsuario = new Entidades.GrupoTrabalhoUsuario();
                    grupoTrabalhoUsuario.GrupoTrabalho = grupoSelecionado;
                    grupoTrabalhoUsuario.Usuario = new Entidades.Usuario() { IdUsuario = this.IdUsuario };
                    if (new Negocios.Diretor().RemoverGrupoTrabalhoDoUsuario(grupoTrabalhoUsuario))
                    {
                        AtualizarGridGrupos(grupoSelecionado, true);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Grupo removido.');", true);
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

        protected void grvGrupo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void AtualizarGridDias(Entidades.DiaSemana dia, bool excluir)
        {
            try
            {
                if (this.IdUsuario > 0)
                {
                    if (excluir && dia != null && dia.IdDia > 0)
                    {
                        List<Entidades.DiaSemana> gridDias = ViewState["dias"] as List<Entidades.DiaSemana>;
                        var consulta = (from f in gridDias
                                        where f.IdDia == dia.IdDia
                                        select f);

                        gridDias.Remove(consulta.First());
                        ViewState["dias"] = gridDias;

                        this.grvDia.DataSource = gridDias;
                        this.grvDia.DataBind();
                    }
                    else if (dia != null)
                    {
                        List<Entidades.DiaSemana> gridDias = ViewState["dias"] as List<Entidades.DiaSemana>;
                        if (gridDias == null)
                            gridDias = new List<Entidades.DiaSemana>();
                        gridDias.Add(dia);

                        ViewState["dias"] = gridDias;
                        this.grvDia.DataSource = gridDias;
                        this.grvDia.DataBind();
                    }
                    else
                    {
                        ViewState["dias"] = new Negocios.Trabalho().DiasTrabalhoPorUsuario(new Entidades.Usuario() { IdUsuario = this.IdUsuario });
                        this.grvDia.DataSource = ViewState["dias"] as List<Entidades.DiaSemana>;
                        this.grvDia.DataBind();
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

        protected void grvDia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    string cod = Convert.ToString(e.CommandArgument);
                    List<Entidades.DiaSemana> diasSelecionados = ViewState["dias"] as List<Entidades.DiaSemana>;
                    Entidades.DiaSemana diaSelecionado = new Entidades.DiaSemana();
                    diaSelecionado.IdDia = Convert.ToInt32(cod);

                    Entidades.DiasTrabalhados diasTrabalhados = new Entidades.DiasTrabalhados();
                    diasTrabalhados.DiaSemana = diaSelecionado;
                    diasTrabalhados.Usuario = new Entidades.Usuario() { IdUsuario = this.IdUsuario };
                    if (new Negocios.Diretor().RemoverDiaTrabalhado(diasTrabalhados))
                    {
                        AtualizarGridDias(diaSelecionado, true);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Dia removido.');", true);
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

        protected void grvDia_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void imgOK_Dia_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.IdUsuario > 0)
                {
                    Entidades.DiasTrabalhados diasTrabalhado = new Entidades.DiasTrabalhados();
                    diasTrabalhado.Usuario = new Entidades.Usuario() { IdUsuario = this.IdUsuario };
                    diasTrabalhado.DiaSemana = new Entidades.DiaSemana() { IdDia = Convert.ToInt32(ddlDia.SelectedValue), Descricao = ddlDia.SelectedItem.Text };
                    if (new Negocios.Diretor().DefinirDiaTrabalhado(diasTrabalhado))
                    {
                        AtualizarGridDias(diasTrabalhado.DiaSemana, false);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Dia adicionado com sucesso.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Dia não adicionado.');", true);
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlTipo.SelectedValue) == (int)Entidades.TipoUsuarioEnum.Editor)
                {
                    this.divDiasTrabalho.Visible = true;
                }
                else
                {
                    this.divDiasTrabalho.Visible = false;
                }
            }
        }

    }
}