using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_EditarNoticia
    {
        Negocios.Usuario NegUsuario;
        Negocios.Noticia NegNoticia;
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
            this.NegUsuario = null;
            this.NegNoticia = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias que contém botão editar, disponível com um usuário que contém a permissão desta:  Mostrar notícias disponíveis para edição;
        [TestMethod]
        public void ComAcesso_Noticias_Para_Edicao()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Editar_Noticia } });

            var retorno = NegNoticia.NoticiasParaEdicao();

            Assert.IsNotNull(retorno);
        }

        //Acessar opção de Visualizar Notícias que contém botão editar disponível  com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Noticias_Para_Edicao()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegNoticia.NoticiasParaEdicao();

            Assert.IsNull(retorno);
        }

        //Alterar dados e submeter edição: sistema apresenta mensagem de sucesso.
        [TestMethod]
        public void Submeter_Edicao_Complemente()
        {
            Negocios.Sessao.NoticiaAtual.IdNoticia = 1;
            Negocios.Sessao.NoticiaAtual.Titulo = "São Paulo";
            Negocios.Sessao.NoticiaAtual.Conteudo = "Melhor Time do Brasil";
            var retorno = NegReporter.SubmeterEdicao();
            Assert.AreEqual(true, retorno);
        }

        //Alterar dados e não preencher um campo obrigatório: sistema apresenta mensagem de erro.
        [TestMethod]
        public void Submeter_Edicao_incomplemente()
        {
            Negocios.Sessao.NoticiaAtual.Titulo = "";
            Negocios.Sessao.NoticiaAtual.Conteudo = "Melhor Time do Brasil";
            var retorno = NegReporter.SubmeterEdicao();
            Assert.AreEqual(false, retorno);
        }
    }
}
