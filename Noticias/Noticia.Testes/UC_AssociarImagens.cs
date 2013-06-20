using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_AssociarImagens
    {
        Negocios.Fotografo NegFotografo;
        Negocios.Noticia NegNoticia;
        Negocios.Diretor NegDiretor;

        [TestInitialize]
        public void IniciarTestes()
        {
            this.NegFotografo = new Negocios.Fotografo();
            this.NegNoticia = new Negocios.Noticia();
            this.NegDiretor = new Negocios.Diretor();
            Negocios.Singleton.IniciarSessao();
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Login = "Bento", Senha = "senha" };
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            this.NegFotografo = null;
            this.NegNoticia = null;
            this.NegDiretor = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Associar imagens com um usuário que contém a permissão desta:  Apresentar a opção de associar imagens;
        [TestMethod]
        public void ComAcesso_Para_AssociarImagens()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Associar_Imagens } });

            var retorno = NegFotografo.TenhoPermissao(Entidades.PermissaoEnum.Associar_Imagens);

            Assert.AreEqual(true, retorno);
        }

        //Acessar opção de Associar imagens com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Para_AssociarImagens()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegFotografo.TenhoPermissao(Entidades.PermissaoEnum.Associar_Imagens);

            Assert.AreEqual(false, retorno);
        }

        //Visualizar notícias atribuídos ao grupo de trabalho e respectivamente ao usuário:  Sistema lista notícias atribuído-os;
        // ****passar o usuario logado e retornará as noticias baseadas no grupo
        //NoticiasDoGrupoTrabalho
        [TestMethod]
        public void ComAcesso_Visualizar_Noticias_Do_GrupoTrabalho()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Associar_Imagens } });

            var retorno = NegNoticia.NoticiasDoGrupoTrabalho();

            Assert.IsNotNull(retorno);
        }

        //Escolha da notícia e vinculação das imagens corretamente: Sistema exibe uma mensagem de sucesso
        // ****Atribuir noticia e imagem (Cada teste, deve ter IDImagem e IdNoticia Diferente, ou apagar teste anterior)
        [TestMethod]
        public void Associar_Imagem()
        {
            Entidades.Noticia noticia = new Entidades.Noticia() { IdNoticia = 1, Titulo = "Copa do Mundo", Conteudo = "Brazil :)" };
            this.NegDiretor.CriarNoticia(noticia);

            Entidades.Imagem imagem = new Entidades.Imagem() { IdImagem = 5 };

            var retorno = NegFotografo.AssociarImagem(noticia, imagem);
            Assert.AreEqual(true, retorno);
        }

    }
}
