using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmManterGrupo : NoticiaPage
    {

        private int IdGrupoTrabalho = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdGrupoTrabalho"] != null && Request.QueryString["IdGrupoTrabalho"].ToString().Length > 0)
                {
                    ViewState["IdGrupoTrabalho"] = Convert.ToInt32(Request.QueryString["IdGrupoTrabalho"]);
                    this.IdGrupoTrabalho = Convert.ToInt32(Convert.ToInt32(ViewState["IdGrupoTrabalho"]));
                    this.CarregarGrupoTrabalho();
                }

            }
            else
            {
                if (ViewState["IdGrupoTrabalho"] != null)
                    this.IdGrupoTrabalho = Convert.ToInt32(ViewState["IdGrupoTrabalho"]);
                else
                    this.IdGrupoTrabalho = 0;
            }
        }

        private void CarregarGrupoTrabalho()
        {
            try
            {
                List<Entidades.GrupoTrabalho> consulta = new Negocios.GrupoTrabalho().Listar(new Entidades.GrupoTrabalho() { IdGrupoTrabalho = this.IdGrupoTrabalho });
                if (consulta.Count > 0)
                {
                    txtDescricao.Text = consulta.First().Descricao;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        private void PreencherGrupoTrabalho(Entidades.GrupoTrabalho GrupoTrabalho)
        {
            GrupoTrabalho.Descricao = txtDescricao.Text;
        }

        protected void btn_salvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Entidades.GrupoTrabalho GrupoTrabalho = new Entidades.GrupoTrabalho();
                //Inserir
                PreencherGrupoTrabalho(GrupoTrabalho);
                if (this.IdGrupoTrabalho == 0)
                {
                    if (!(new Negocios.Diretor().ManterGrupoTrabalho(GrupoTrabalho, Negocios.Singleton.CRUDEnum.INSERIR)))
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
                    if (!(new Negocios.Diretor().ManterGrupoTrabalho(GrupoTrabalho, Negocios.Singleton.CRUDEnum.ALTERAR)))
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

        protected void btnNovo_Click(object sender, ImageClickEventArgs e)
        {
            txtDescricao.Text = String.Empty;
        }
    }
}