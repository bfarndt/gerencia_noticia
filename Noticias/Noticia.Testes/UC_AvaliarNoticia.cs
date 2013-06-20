using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_AvaliarNoticia
    {
        Negocios.Noticia NegNoticia;
        Negocios.Usuario NegUsuario;
        Negocios.Editor NegEditor;

        [TestInitialize]
        public void IniciarTestes()
        {
            Negocios.Singleton.IniciarSessao();
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento", Senha = "senha" };
            this.NegUsuario = new Negocios.Usuario();
            this.NegUsuario.Logar();
            this.NegNoticia = new Negocios.Noticia();
            this.NegEditor = new Negocios.Editor();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            NegNoticia = null;
            NegUsuario = null;
            NegEditor = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias a serem avaliadas com um usuário que contém a permissão desta:  Apresentar as notícias a serem avaliadas;
        [TestMethod]
        public void ComAcesso_Visualizar_Noticias_A_serem_Avaliadas()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Avaliar_Noticia } });

            var retorno = NegNoticia.NoticiasParaAvaliacao();

            Assert.IsNotNull(retorno);
        }

        //Acessar opção de Visualizar Notícias a serem avaliadas com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Visualizar_Noticias_A_serem_Avaliadas()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegNoticia.NoticiasParaAvaliacao();

            Assert.IsNull(retorno);
        }

        //Confirmar recusa de notícia: sistema apresenta mensagem de sucesso.
        [TestMethod]
        public void Aprovar_Noticia()
        {
            Entidades.Noticia noticia = new Entidades.Noticia();
            noticia.IdNoticia = 1;
            noticia.Titulo = "São Paulo";
            noticia.Conteudo = "Melhor Time do Brasil";
            var retorno = NegEditor.AprovarNoticia(noticia, "Ficou boa");
            Assert.AreEqual(true, retorno);
        }

        //Confirmar recusa de notícia sem adicionar feedback: sistema apresenta mensagem de erro.
        [TestMethod]
        public void Reprovar_Noticia()
        {
            Entidades.Noticia noticia = new Entidades.Noticia();
            noticia.IdNoticia = 1;
            noticia.Titulo = "São Paulo";
            noticia.Conteudo = "Melhor Time do Brasil";
            var retorno = NegEditor.ReprovarNoticia(noticia,"Ficou Ruim");
            Assert.AreEqual(true, retorno);
        }

    }
}
