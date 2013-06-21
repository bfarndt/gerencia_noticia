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
                if (Request.QueryString["IdUsuario"] != null && Request.QueryString["IdUsuario"].ToString().Length > 0)
                {
                    ViewState["IdUsuario"] = Convert.ToInt32(Request.QueryString["IdUsuario"]);
                    this.IdUsuario = Convert.ToInt32(Convert.ToInt32(ViewState["IdUsuario"]));
                    this.CarregarUsuario();
                }

                this.CarregarCombos();
            }
            else
            {
                if (ViewState["IdUsuario"] != null)
                    this.IdUsuario = Convert.ToInt32(ViewState["IdUsuario"]);
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
                    }else
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
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        private void PreencherUsuario(Entidades.Usuario usuario)
        {
            usuario.TipoUsuario = new Entidades.TipoUsuario() { IdTipoUsuario = Convert.ToInt32(this.ddlTipo.SelectedValue), Descricao = this.ddlTipo.Text };
            usuario.Login = txtLogin.Text;
            usuario.Senha = txtSenha.Text;
            usuario.Nome = txtNome.Text;
            usuario.UsuarioEndereco = new Entidades.UsuarioEndereco() { Email = txtEmail.Text, Telefone = txtTelefone.Text };
            usuario.Contratacao = new Entidades.Contratacao() { DataHora = DateTime.Now };
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message +"');", true);
            }
        }

    }
}