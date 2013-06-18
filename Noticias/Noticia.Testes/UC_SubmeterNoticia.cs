using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_SubmeterNoticia
    {
        Negocios.Noticia NegNoticia;
        Negocios.Usuario NegUsuario;
        Negocios.Reporter NegReporter;

        [TestInitialize]
        public void IniciarTestes()
        {
            Negocios.Sessao.IniciarSessao();
            Negocios.Sessao.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento", Senha = "senha" };
            Negocios.Sessao.NoticiaAtual = new Entidades.Noticia();
            this.NegUsuario = new Negocios.Usuario();
            this.NegUsuario.EfetuarAcesso();
            this.NegNoticia = new Negocios.Noticia();
            this.NegReporter = new Negocios.Reporter();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            NegNoticia = null;
            NegUsuario = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias com um usuário que contém a permissão desta:  Apresentar as notícias permitidas;
        [TestMethod]
        public void ComAcesso_Visualizar_Noticias_A_serem_Submetidas()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Submeter_Noticia } });

            var retorno = NegNoticia.NoticiasParaSubmissao();

            Assert.IsNotNull(retorno);
        }

        //Acessar opção de Visualizar Notícias com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Visualizar_Noticias_A_serem_Submetidas()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegNoticia.NoticiasParaSubmissao();

            Assert.IsNull(retorno);
        }

        //Selecionar a opção de submeter notícia com todos os dados preenchidos corretamente: sistema apresenta mensagem de sucesso.
        [TestMethod]
        public void Submeter_Noticia_Com_Sucesso() 
        {
            Negocios.Sessao.NoticiaAtual.IdNoticia = 1;
            Negocios.Sessao.NoticiaAtual.Titulo = "São Paulo";
            Negocios.Sessao.NoticiaAtual.Conteudo = "Melhor Time do Brasil";
            var retorno = NegReporter.SubmeterNoticia();
            Assert.AreEqual(true, retorno);
        }
        
        //Selecionar a opção de submeter notícia com dados incorretos: sistema apresenta mensagem de erro.
        [TestMethod]
        public void Submeter_Noticia_Com_Falha()
        {
            Negocios.Sessao.NoticiaAtual.Titulo = "";
            Negocios.Sessao.NoticiaAtual.Conteudo = "Melhor Time do Brasil";
            var retorno = NegReporter.SubmeterNoticia();
            Assert.AreEqual(false, retorno);
        }
    }
}
