using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmTrabalho : NoticiaPage
    {
        private bool blnModoEdicao;
        private bool blnDataSourceVazio;

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.CarregarGridView();
            }
        }

        private void CarregarGridView()
        {
            try
            {
                Entidades.Trabalho trabalho = new Entidades.Trabalho();

                List<Entidades.Trabalho> listaDeTrabalhos = new Negocios.Trabalho().Listar();

                if (listaDeTrabalhos.Count == 0)
                {
                    listaDeTrabalhos.Add(trabalho);
                    this.blnDataSourceVazio = true;
                }

                this.grvTrabalho.DataSource = listaDeTrabalhos;
                this.grvTrabalho.DataBind();

            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, "Erro no grid: " + ex.Message);
            }
        }

        protected void grvTrabalho_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvTrabalho.EditIndex = -1;
            this.blnModoEdicao = false;
            this.CarregarGridView();
        }

        protected void grvTrabalho_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grvTrabalho_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grvTrabalho_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.blnModoEdicao = true;
            this.grvTrabalho.EditIndex = e.NewEditIndex;
            this.CarregarGridView();
        }

        protected void grvTrabalho_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grv = sender as GridView;

            Entidades.Trabalho trabalho = new Entidades.Trabalho();

            trabalho.IdTrabalho = Convert.ToInt32(this.grvTrabalho.DataKeys[e.RowIndex].Value.ToString());

            if (!string.IsNullOrEmpty(((TextBox)grv.Rows[e.RowIndex].FindControl("txtValorHoraEdit")).Text))
            {
                decimal result;
                if (decimal.TryParse(((TextBox)grv.Rows[e.RowIndex].FindControl("txtValorHoraEdit")).Text, out result))
                {
                    trabalho.ValorHoraTrabalhada = result;
                }
            }

            if (new Negocios.Diretor().ManterTrabalho(trabalho, Negocios.Singleton.CRUDEnum.ALTERAR))
            {
                this.ExibirMensagem(TipoMensagem.Sucesso, "Valor definido com sucesso!");
            }
            else
            {
                this.ExibirMensagem(TipoMensagem.Erro, "Operação não realizada!");
            }

            this.grvTrabalho.EditIndex = -1;
            this.blnModoEdicao = false;
            this.CarregarGridView();
        }

        private Negocios.Trabalho trp = new Negocios.Trabalho();

        protected void grvTrabalho_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }

            if (this.blnModoEdicao && e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Visible = false;
            }

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                if (this.blnDataSourceVazio)
                {
                    e.Row.Visible = false;
                }

                //Visibilidade da linha
                if ((e.Row.RowType == DataControlRowType.DataRow) && !(this.blnModoEdicao))
                {
                    e.Row.Visible = !this.blnDataSourceVazio;

                }
                else
                {
                    e.Row.Visible = !(e.Row.RowType == DataControlRowType.Footer && this.blnModoEdicao);
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

            if (e.Row.RowType == DataControlRowType.DataRow && this.blnDataSourceVazio)
            {
                e.Row.Visible = false;
            }
        }

        #endregion
    }
}