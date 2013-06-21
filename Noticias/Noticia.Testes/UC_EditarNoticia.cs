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
            this.NegUsuario = null;
            this.NegNoticia = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias que contém botão editar, disponível com um usuário que contém a permissão desta:  Mostrar notícias disponíveis para edição;
        [TestMethod]
        public void ComAcesso_Noticias_Para_Edicao()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Editar_Noticia } });

            var retorno = NegNoticia.NoticiasParaEdicao();

            Assert.IsNotNull(retorno);
        }

        //Acessar opção de Visualizar Notícias que contém botão editar disponível  com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Noticias_Para_Edicao()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegNoticia.NoticiasParaEdicao();

            Assert.IsNull(retorno);
        }

        //Alterar dados e submeter edição: sistema apresenta mensagem de sucesso.
        [TestMethod]
        public void Submeter_Edicao_Complemente()
        {
            Entidades.Noticia noticia = new Entidades.Noticia();
            noticia.IdNoticia = 1;
            noticia.Titulo = "São Paulo";
            noticia.Conteudo = "Melhor Time do Brasil";
            noticia.PalavrasChave = new List<Entidades.PalavraChave>();
            noticia.PalavrasChave.Add(new Entidades.PalavraChave() { Noticia = noticia, PalavraChaveTexto = "Qualquer" });
            noticia.PalavrasChave.Add(new Entidades.PalavraChave() { Noticia = noticia, PalavraChaveTexto = "QualquerOUtra" });
            var retorno = NegReporter.EditarNoticia(noticia);
            Assert.AreEqual(true, retorno);
        }

        //Alterar dados e não preencher um campo obrigatório: sistema apresenta mensagem de erro.
        [TestMethod]
        public void Submeter_Edicao_incomplemente()
        {
            Entidades.Noticia noticia = new Entidades.Noticia();
            noticia.Titulo = "";
            noticia.Conteudo = "Melhor Time do Brasil";
            var retorno = NegReporter.EditarNoticia(noticia);
            Assert.AreEqual(false, retorno);
        }
    }
}
