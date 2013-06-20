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
            Negocios.Singleton.IniciarSessao();
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento", Senha = "senha" };
            this.NegUsuario = new Negocios.Usuario();
            this.NegUsuario.CarregarPermissoes();
            this.NegNoticia = new Negocios.Noticia();
            this.NegReporter = new Negocios.Reporter();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            this.NegNoticia = null;
            this.NegUsuario = null;
            this.NegReporter = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias com um usuário que contém a permissão desta:  Apresentar as notícias permitidas;
        [TestMethod]
        public void ComAcesso_Visualizar_Noticias_A_serem_Submetidas()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Submeter_Noticia } });

            var retorno = NegNoticia.NoticiasParaSubmissao();

            Assert.IsNotNull(retorno);
        }

        //Acessar opção de Visualizar Notícias com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Visualizar_Noticias_A_serem_Submetidas()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegNoticia.NoticiasParaSubmissao();

            Assert.IsNull(retorno);
        }

        //Selecionar a opção de submeter notícia com todos os dados preenchidos corretamente: sistema apresenta mensagem de sucesso.
        [TestMethod]
        public void Submeter_Noticia_Com_Sucesso()
        {
            Entidades.Noticia noticia = new Entidades.Noticia();
            noticia.IdNoticia = 1;
            noticia.Titulo = "São Paulo";
            noticia.Conteudo = "Melhor Time do Brasil";
            noticia.PalavrasChave = new List<Entidades.PalavraChave>();
            noticia.PalavrasChave.Add(new Entidades.PalavraChave() { Noticia = noticia, PalavraChaveTexto = "Qualquer" });
            noticia.PalavrasChave.Add(new Entidades.PalavraChave() { Noticia = noticia, PalavraChaveTexto = "QualquerOUtra" });

            var retorno = NegReporter.SubmeterNoticia(noticia);
            Assert.AreEqual(true, retorno);
        }

        //Selecionar a opção de submeter notícia com dados incorretos: sistema apresenta mensagem de erro.
        [TestMethod]
        public void Submeter_Noticia_Com_Falha()
        {
            Entidades.Noticia noticia = new Entidades.Noticia();
            noticia.Titulo = "";
            noticia.Conteudo = "Melhor Time do Brasil";
            var retorno = NegReporter.SubmeterNoticia(noticia);
            Assert.AreEqual(false, retorno);
        }
    }
}
