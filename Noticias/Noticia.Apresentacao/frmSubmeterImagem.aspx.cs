using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noticia.Apresentacao
{
    public partial class frmSubmeterImagem : NoticiaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AtualizarGridImagens(null, false);
            }
        }

        private void AtualizarGridImagens(Entidades.ImagemArquivo arquivo, bool excluir)
        {
            try
            {
                if (excluir && arquivo != null && arquivo.Imagem != null && arquivo.Imagem.IdImagem > 0)
                {
                    List<Entidades.ImagemArquivo> gridImagens = ViewState["imagens"] as List<Entidades.ImagemArquivo>;
                    var consulta = (from f in gridImagens
                                    where f.Imagem.IdImagem == arquivo.Imagem.IdImagem
                                    select f);

                    gridImagens.Remove(consulta.First());
                    ViewState["imagens"] = gridImagens;

                    this.grvImagens.DataSource = gridImagens;
                    this.grvImagens.DataBind();
                }
                else if (arquivo != null)
                {
                    List<Entidades.ImagemArquivo> gridImagens = ViewState["imagens"] as List<Entidades.ImagemArquivo>;
                    if (gridImagens == null)
                        gridImagens = new List<Entidades.ImagemArquivo>();
                    gridImagens.Add(arquivo);

                    ViewState["imagens"] = gridImagens;
                    this.grvImagens.DataSource = gridImagens;
                    this.grvImagens.DataBind();
                }
                else
                {
                    ViewState["imagens"] = new Negocios.Imagem().ImagensNaoSelecionadas();
                    this.grvImagens.DataSource = ViewState["imagens"] as List<Entidades.ImagemArquivo>;
                    this.grvImagens.DataBind();
                }


            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }


        protected void AsyncFileUpload2_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {
                if (e.State == AjaxControlToolkit.AsyncFileUploadState.Success)
                {
                    string dir = @"C:/upload/";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string filePath = dir + e.FileName;
                    if (!File.Exists(filePath))
                    {
                        AsyncFileUpload2.SaveAs(filePath);
                        FileInfo file = new FileInfo(filePath);
                        if (new Negocios.Fotografo().SubmeterImagem(file))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Imagem submetida.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('Imagem Inválida.');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "aler", "alert('" + ex.Message + "');", true);
            }
        }

        protected void AsyncFileUpload2_UploadedFileError(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {

        }

        protected void grvImagens_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim().ToUpper() == "EXCLUIR")
                {
                    string cod = Convert.ToString(e.CommandArgument);
                    List<Entidades.ImagemArquivo> selecionados = ViewState["imagens"] as List<Entidades.ImagemArquivo>;
                    Entidades.ImagemArquivo selecionado = new Entidades.ImagemArquivo();
                    selecionado.IdImagemArquivo = Convert.ToInt32(cod);

                    if (new Negocios.Fotografo().DeletarImagem(selecionado))
                    {
                        AtualizarGridImagens(null, false);
                        ExibirMensagem(TipoMensagem.Sucesso, "Imagem removida.");
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Sucesso, "Não foi possível completar a operação.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        protected void grvImagens_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void imgAtualizar_Click(object sender, ImageClickEventArgs e)
        {
            AtualizarGridImagens(null, false);
        }

        protected void btnVoltar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}