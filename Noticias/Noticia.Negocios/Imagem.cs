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

        public List<Entidades.Imagem> ImagensDeNoticiasAssociadas()
        {
            try
            {
                if (NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Selecionar_Imagens))
                {
                    List<Entidades.Imagem> imagensAssociadas = new List<Entidades.Imagem>();

                    Entidades.GrupoTrabalhoUsuario consultaPorUsuario = new Entidades.GrupoTrabalhoUsuario();
                    consultaPorUsuario.Usuario = Singleton.UsuarioLogado;

                    Entidades.NoticiaGrupoTrabalho consultaPorGrupo;
                    Entidades.NoticiaImagem consultaPorNoticia;

                    foreach (var grupo in dalGrupoTrabalhoUsuario.Consultar(consultaPorUsuario))
                    {
                        consultaPorGrupo = new Entidades.NoticiaGrupoTrabalho();
                        consultaPorGrupo.GrupoTrabalho = grupo.GrupoTrabalho;

                        foreach (var noticia in dalNoticiaGrupoTrabalho.Consultar(consultaPorGrupo))
                        {
                            consultaPorNoticia = new Entidades.NoticiaImagem();
                            consultaPorNoticia.Noticia = noticia.Noticia;

                            foreach (var imagem in dalNoticiaImagem.Consultar(consultaPorNoticia))
                            {
                                imagensAssociadas.Add(imagem.Imagem);
                            }
                        }
                    }

                    return imagensAssociadas;
                }
                else
                {
                    return null;
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
    }
}
