using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Noticia.Negocios
{
    public class Imagem
    {
        Negocios.Usuario NegUsuario = new Usuario();
        AcessoDados.GrupoTrabalhoUsuario dalGrupoTrabalhoUsuario = new AcessoDados.GrupoTrabalhoUsuario();
        AcessoDados.NoticiaGrupoTrabalho dalNoticiaGrupoTrabalho = new AcessoDados.NoticiaGrupoTrabalho();
        AcessoDados.NoticiaImagem dalNoticiaImagem = new AcessoDados.NoticiaImagem();

        public List<string> ExtensoesValidas { get; set; }

        public Imagem()
        {
            this.ExtensoesValidas = new List<string>();
            this.CarregarExtensoes();
        }

        private void CarregarExtensoes()
        {
            this.ExtensoesValidas = new List<string>();
            this.ExtensoesValidas.Add(".jpg");
            this.ExtensoesValidas.Add(".jpeg");
            this.ExtensoesValidas.Add(".png");
        }

        public bool ValidarExtensao(FileInfo file)
        {
            return this.ExtensoesValidas.Contains(file.Extension);
        }

        public bool ValidarTamanho(FileInfo file)
        {
            //2MB = 2.000.000
            return file.Length < 2000000;
        }

        public byte[] RetornarArrayBytes(FileInfo file)
        {
            FileStream fs = file.OpenRead();

            int nBytes = (int)file.Length;
            byte[] ByteArray = new byte[nBytes];
            int nBytesRead = fs.Read(ByteArray, 0, nBytes);

            return ByteArray;
        }

        public bool ValidarImagem(Entidades.Imagem imagem)
        {
            return !(string.IsNullOrWhiteSpace(imagem.Legenda));
        }

        public List<Entidades.ImagemArquivo> ImagensNaoSelecionadas()
        {
            try
            {
                List<Entidades.ImagemArquivo> retorno = new List<Entidades.ImagemArquivo>();

                foreach (var item in new AcessoDados.ImagemArquivo().Consultar(new Entidades.ImagemArquivo() { Imagem = new Entidades.Imagem() { IdImagem = null } }))
                {
                    if (item.Imagem.Selecionada.Value)
                        continue;

                    item.Imagem.Legenda = item.NomeArquivo;
                    retorno.Add(item);
                }
                return retorno;
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

        public Entidades.ImagemArquivo CarregarImagemArquivo(Entidades.Imagem imagem)
        {
            try
            {
                Entidades.ImagemArquivo retorno = new Entidades.ImagemArquivo();
                var consulta = new AcessoDados.Imagem().Consultar(imagem);
                if (consulta.Count > 0)
                {
                    retorno.Imagem = consulta.First();
                }

                return retorno;
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

        public byte[] CarregarImagem(Entidades.Imagem imagem)
        {
            try
            {
                return new AcessoDados.ImagemArquivo().CarregarImagem(new Entidades.ImagemArquivo() { Imagem = imagem });
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

        public List<Entidades.Imagem> ImagensSelecionadasDaNoticia(int IdNoticia)
        {
            try
            {
                List<Entidades.Imagem> retorno = new List<Entidades.Imagem>();

                var found = from f in new Negocios.Noticia().NoticiasImagensSelecionadas()
                            where f.Noticia.IdNoticia == IdNoticia
                            select f;

                foreach (var item in found)
                {
                    retorno.Add(item.Imagem);
                }

                return retorno;
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
