using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Noticia.Negocios
{
    public class Fotografo : Usuario
    {
        AcessoDados.Imagem dalImagem = new AcessoDados.Imagem();
        AcessoDados.ImagemArquivo dalImagemArquivo = new AcessoDados.ImagemArquivo();
        AcessoDados.NoticiaImagem dalNoticiaImagem = new AcessoDados.NoticiaImagem();
        AcessoDados.Historico dalHistorico = new AcessoDados.Historico();
        

        Negocios.Imagem NegImagem = new Imagem();

        public bool SubmeterImagem(FileInfo file)
        {
            try
            {
                if (NegImagem.ValidarExtensao(file) && NegImagem.ValidarTamanho(file))
                {
                    string strRetorno = string.Empty;

                    Entidades.Imagem imagem = new Entidades.Imagem();
                    //Inserir apenas IdImagem
                    strRetorno = dalImagem.Inserir(imagem);

                    int intResult = 0;
                    if (int.TryParse(strRetorno, out intResult))
                    {
                        imagem.IdImagem = intResult;
                        Entidades.ImagemArquivo imagemArquivo = new Entidades.ImagemArquivo();
                        imagemArquivo.Imagem = imagem;
                        imagemArquivo.Extensao = file.Extension;
                        imagemArquivo.Tamanho = file.Length.ToString();
                        imagemArquivo.Formato = "SEILA";
                        imagemArquivo.ImagemBytes = NegImagem.RetornarArrayBytes(file);

                        strRetorno = dalImagemArquivo.Inserir(imagemArquivo);

                        return int.TryParse(strRetorno, out intResult);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }
        }

        public bool AssociarImagem(Entidades.Noticia noticia, Entidades.Imagem imagem)
        {
            try
            {
                //Executar insert
                string strRetorno = string.Empty;

                Entidades.NoticiaImagem noticiaImagem = new Entidades.NoticiaImagem();
                noticiaImagem.Noticia = noticia;
                noticiaImagem.Imagem = imagem;

                strRetorno = dalNoticiaImagem.Inserir(noticiaImagem);
                

                int intResult = 0;

                if (int.TryParse(strRetorno, out intResult))
                {
                    Entidades.Historico historico = new Entidades.Historico();
                    historico.Noticia = noticia;
                    historico.Usuario = Singleton.UsuarioLogado;
                    historico.DataHora = DateTime.Now;
                    historico.StatusNoticia = new Entidades.StatusNoticia() { IdStatus = (int)Entidades.StatusNoticiaEnum.ImagensAssociadas };

                    strRetorno = dalHistorico.Inserir(historico);
                }

                return (int.TryParse(strRetorno, out intResult));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AcessoDados.Dados.FecharConexao();
            }
        }

    }
}
