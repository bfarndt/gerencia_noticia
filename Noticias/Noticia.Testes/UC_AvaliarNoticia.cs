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
            Negocios.Sessao.IniciarSessao();
            Negocios.Sessao.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento", Senha = "senha" };
            Negocios.Sessao.NoticiaAtual = new Entidades.Noticia();
            this.NegUsuario = new Negocios.Usuario();
            this.NegUsuario.EfetuarAcesso();
            this.NegNoticia = new Negocios.Noticia();
            this.NegEditor = new Negocios.Editor();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            NegNoticia = null;
            NegUsuario = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias a serem avaliadas com um usuário que contém a permissão desta:  Apresentar as notícias a serem avaliadas;
        [TestMethod]
        public void ComAcesso_Visualizar_Noticias_A_serem_Avaliadas()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Avaliar_Noticia } });

            var retorno = NegNoticia.NoticiasParaAvaliacao();

            Assert.IsNotNull(retorno);
        }

        //Acessar opção de Visualizar Notícias a serem avaliadas com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Visualizar_Noticias_A_serem_Avaliadas()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegNoticia.NoticiasParaAvaliacao();

            Assert.IsNull(retorno);
        }

        //Confirmar recusa de notícia: sistema apresenta mensagem de sucesso.
        [TestMethod]
        public void Aprovar_Noticia()
        {
            Negocios.Sessao.NoticiaAtual.IdNoticia = 1;
            Negocios.Sessao.NoticiaAtual.Titulo = "São Paulo";
            Negocios.Sessao.NoticiaAtual.Conteudo = "Melhor Time do Brasil";
            var retorno = NegEditor.AprovarNoticia("Ficou boa");
            Assert.AreEqual(true, retorno);
        }

        //Confirmar recusa de notícia sem adicionar feedback: sistema apresenta mensagem de erro.
        [TestMethod]
        public void Reprovar_Noticia()
        {
            Negocios.Sessao.NoticiaAtual.IdNoticia = 1;
            Negocios.Sessao.NoticiaAtual.Titulo = "São Paulo";
            Negocios.Sessao.NoticiaAtual.Conteudo = "Melhor Time do Brasil";
            var retorno = NegEditor.ReprovarNoticia("Ficou Ruim");
            Assert.AreEqual(true, retorno);
        }

    }
}
