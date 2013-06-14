using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_EfetuarAcesso
    {
        private Negocios.Usuario NegUsuario;

        [TestInitialize]
        public void IniciarTestes()
        {
            this.NegUsuario = new Negocios.Usuario();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            this.NegUsuario = null;
            Console.WriteLine("Finalizando testes");
        }

        //Digitar um login e senha existentes no banco de dados: É validado e apresentada tela principal;
        [TestMethod]
        public void Login_Senha_Existente()
        {
            this.NegUsuario.usuarioLogado = new Entidades.Usuario();
            var retorno = NegUsuario.ValidarUsuario();
            Assert.AreEqual(true, retorno);
        }

        //Digitar um login e senha inexistentes no banco de dados: É apresentada mensagem de login e senha incorretos;
        [TestMethod]
        public void Login_Senha_Inexistente()
        {
            this.NegUsuario.usuarioLogado = null;
            var retorno = NegUsuario.ValidarUsuario();
            Assert.AreEqual(false, retorno);
        }

        //Clicar nas opções estando logado no sistema: Opções respectivas são apresentadas;
        [TestMethod]
        public void Retorno_Opcoes_Logado()
        {
            this.NegUsuario.usuarioLogado = new Entidades.Usuario();
            var retorno = NegUsuario.Permissoes();
            Assert.IsNotNull(retorno, "Com permissões");
        }

        //Clicar nas opções sem ter tido lgado no sistema: Apresentar tela de login;
        [TestMethod]
        public void Retorno_Nao_Logado()
        {
            this.NegUsuario.usuarioLogado = null;
            var retorno = NegUsuario.Permissoes();
            Assert.IsNull(retorno, "Sem permissões");
        }

        //Acessar constantemente o sistema: Sistema não expirará sessão;
        [TestMethod]
        public void ComAcesso_CincoMinutos()
        {
            NegUsuario.usuarioLogado = new Entidades.Usuario();
            NegUsuario.ValidarUsuario();
            Thread.Sleep(1); //Tempo de espera
            var retorno = NegUsuario.ComSessao();

            Assert.AreEqual(true, retorno);
        }

        //Ficar sem acessar o sistema por 5 min contínuos: Sistema  expirará sessão e apresentará tela de login;
        [TestMethod]
        public void SemAcesso_CincoMinutos()
        {
            NegUsuario.usuarioLogado = new Entidades.Usuario();
            NegUsuario.ValidarUsuario();
            Thread.Sleep(2000);//Tempo de espera
            var retorno = NegUsuario.ComSessao();

            Assert.AreEqual(false, retorno);
        }
    }
}