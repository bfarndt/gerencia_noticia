using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public class NoticiaPage : System.Web.UI.Page
    {
        protected virtual void Page_Init(object sender, EventArgs e)
        {
            String url_solicitada = this.Page.Request.Url.ToString();
            url_solicitada = url_solicitada.Remove(0, url_solicitada.LastIndexOf("/") + 1);

            if ((Session["NomeUsuario"] == null && url_solicitada.ToLower() != "login.aspx") && !Negocios.Singleton.comSessao)
            {
                ExibirMensagem(TipoMensagem.Informacao, "Sessão expirada");
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                if (!url_solicitada.Contains("frmManterUsuario.aspx"))
                    (Master.FindControl("litUsuarioLogado") as Literal).Text = "" + Session["NomeUsuario"];
            }

            this.RegistrarHs();
        }


        public void RegistrarHs()
        {

            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "highslide", Page.ResolveClientUrl("~/") + "Scripts/highslide-4.1.8/highslide/highslide-full.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery", Page.ResolveClientUrl("~/") + "Scripts/jquery-1.4.1.min.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery-ui-personalized", Page.ResolveClientUrl("~/") + "Scripts/jquery-ui-personalized-1.5.2.packed.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "sprinkle", Page.ResolveClientUrl("~/") + "Scripts/sprinkle.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "tooltip", Page.ResolveClientUrl("~/") + "Scripts/jquery.tooltip.min.js");

            StringBuilder str = new StringBuilder();
            str.AppendLine("hs.graphicsDir = '" + Page.ResolveClientUrl("~/") + "Scripts/highslide-4.1.8/highslide/graphics/" + "';");
            str.AppendLine("hs.outlineType = 'rounded-white';");
            str.AppendLine("hs.wrapperClassName = 'draggable-header';");
            str.AppendLine("hs.dimmingOpacity = 0.75;");
            str.AppendLine("hs.align = 'center';");
            str.AppendLine("hs.loadingText = 'Carregando...';");
            str.AppendLine("hs.cacheAjax = false;");
            str.AppendLine("hs.onDimmerClick = function () {return false;}");
            str.AppendLine("hs.wrapperClassName = 'draggable-header';");
            str.AppendLine("hs.cssDirection = 'ltr';");
            str.AppendLine("hs.loadingText = 'Aguarde';");
            str.AppendLine("hs.loadingTitle = 'Cancelar';");
            str.AppendLine("hs.moveText = 'Mover';");
            str.AppendLine("hs.closeTitle = 'Fechar (esc)';");
            str.AppendLine("hs.moveTitle = 'Mover';");
            str.AppendLine("hs.fullExpandText = '1=1';");
            str.AppendLine("hs.expandCursor = 'zoomin.cur'; // null disables");
            str.AppendLine("hs.restoreCursor = 'zoomout.cur'; // null disables");
            str.AppendLine("hs.expandDuration = 250; // milliseconds");
            str.AppendLine("hs.restoreDuration = 250;");
            str.AppendLine("hs.marginLeft = 15;");
            str.AppendLine("hs.marginRight = 15;");
            str.AppendLine("hs.marginTop = 15;");
            str.AppendLine("hs.marginBottom = 15;");
            str.AppendLine("hs.zIndexCounter = 1001; // adjust to other absolutely positioned elements");
            str.AppendLine("hs.loadingOpacity = 0.75;");
            str.AppendLine("hs.allowMultipleInstances = true;");
            str.AppendLine("hs.numberOfImagesToPreload = 5;");
            str.AppendLine("hs.outlineWhileAnimating = 2; // 0 = never; 1 = always; 2 = HTML only");
            str.AppendLine("hs.outlineStartOffset = 3; // ends at 10");
            str.AppendLine("hs.padToMinWidth = false; // pad the popup width to make room for wide caption");
            str.AppendLine("hs.fullExpandPosition = 'bottom right';");
            str.AppendLine("hs.fullExpandOpacity = 1;");
            str.AppendLine("hs.showCredits = false; // you can set this to false if you want");
            str.AppendLine("hs.creditsHref = 'javascript:;';");
            str.AppendLine("hs.enableKeyListener = true;");
            str.AppendLine("hs.openerTagNames = ['a']; // Add more to allow slideshow indexing");
            str.AppendLine("hs.allowWidthReduction = false;");
            str.AppendLine("hs.allowHeightReduction = false;");
            str.AppendLine("hs.preserveContent = false;");
            str.AppendLine("hs.objectLoadTime = 'before';");
            str.AppendLine("hs.cacheAjax = false;");
            str.AppendLine("hs.dragByHeading = true;");
            str.AppendLine("hs.minWidth = 450;");
            str.AppendLine("hs.minHeight = 500;");

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "modal", str.ToString(), true);

        }


        /// <summary>
        /// Evento load Complete da página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "format", Page.ResolveClientUrl("~/Scripts/Funcoes.js"));
            ScriptManager.RegisterStartupScript(this, typeof(string), "lib", "setTimeout('aplicacarFormatacaoCampos(document.forms[0]);', 2000);", true);
        }

        private void RegistrarHighslide()
        {
            StringBuilder str = new StringBuilder();
            str.Append("function RegistraHighslide(){hs.graphicsDir = '" + Page.ResolveClientUrl("~/Scripts/highslide/graphics/") + "';");
            str.Append("hs.outlineType = 'rounded-white';");
            str.Append("hs.wrapperClassName = 'draggable-header';");
            str.Append("hs.dimmingOpacity = 0.75;");
            str.Append("hs.onDimmerClick = function() {");
            str.Append("return false;");
            str.Append("}}; setTimeout('RegistraHighslide()',2000);");
            ScriptManager.RegisterStartupScript(this, typeof(string), "hs", str.ToString(), true);
        }

        public void AbrirModal(string url, string width, string titulo)
        {

            StringBuilder str = new StringBuilder();

            str.Append("var modal = 1; $(document).ready(function () {");
            str.Append("hs.htmlExpand(null, {");
            str.Append("src: '" + url + "', objectType: 'iframe', headingText:'" + titulo + "', width:" + width);
            str.Append("});");
            str.Append("});");

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "janela", str.ToString(), true);

        }

        public void AbrirModal(string url, string width, string titulo, string height)
        {

            StringBuilder str = new StringBuilder();

            str.Append("var modal = '1'; $(document).ready(function () {");
            str.Append("hs.htmlExpand(null, {");
            str.Append("src: '" + url + "', objectType: 'iframe', headingText:'" + titulo + "', width:" + width + ",height:" + height);
            str.Append("});");
            str.Append("});");

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "janela", str.ToString(), true);

        }

        public enum TipoMensagem { Alerta, Sucesso, Erro, Informacao };

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem)
        {

            switch (tipoMensagem)
            {
                case TipoMensagem.Alerta:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "warning";
                    break;
                case TipoMensagem.Erro:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "error";
                    break;
                case TipoMensagem.Informacao:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "info";
                    break;
                case TipoMensagem.Sucesso:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "success";
                    break;
            }

            Label lblMensagem = (Master.FindControl("lblMensagem") as Label);
            lblMensagem.Text = mensagem;

            (Master.FindControl("pnlMensagem") as Panel).Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(string), "msg", "$(\"#" + (Master.FindControl("pnlMensagem") as Panel).ClientID + "\").slideDown(500);", true);

            (Master.FindControl("updMensagem") as UpdatePanel).Update();

        }

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem, int timeout)
        {
            this.ExibirMensagem(tipoMensagem, mensagem);
            ScriptManager.RegisterStartupScript(this, typeof(string), "msgTimeout", "setTimeout('$(\"#" + (Master.FindControl("pnlMensagem") as Panel).ClientID + "\").slideUp(500);','5000');", true);
        }

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        public void ExibirMensagemEmOutraPagina(TipoMensagem tipoMensagem, string mensagem)
        {
            System.Collections.Generic.Dictionary<TipoMensagem, string> msg = new Dictionary<TipoMensagem, string>();
            msg.Add(tipoMensagem, mensagem);
            this.Session["MensagemOutraPagina"] = msg;
        }

        /// <summary>
        /// Ocultar mensagem.
        /// </summary>
        public void OcultarMensagem()
        {
            (Master.FindControl("pnlMensagem") as Panel).Visible = false;
            (Master.FindControl("updMensagem") as UpdatePanel).Update();
        }
    }
}