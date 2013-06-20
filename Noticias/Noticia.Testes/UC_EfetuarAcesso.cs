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
            Negocios.Singleton.IniciarSessao();
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
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { Login = "Bento", Senha = "senha" };
            var retorno = NegUsuario.Logar();
            Assert.AreEqual(true, retorno);
        }

        //Digitar um login e senha inexistentes no banco de dados: É apresentada mensagem de login e senha incorretos;
        [TestMethod]
        public void Login_Senha_Inexistente()
        {
            Negocios.Singleton.UsuarioLogado = null;
            var retorno = NegUsuario.Logar();
            Assert.AreEqual(false, retorno);
        }

        //Clicar nas opções estando logado no sistema: Opções respectivas são apresentadas;
        [TestMethod]
        public void Retorno_Opcoes_Logado()
        {
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Login = "Bento", Senha = "senha" };
            var retorno = NegUsuario.Logar(); ;
            Assert.AreEqual(true, retorno);
        }

        //Clicar nas opções sem ter tido lgado no sistema: Apresentar tela de login;
        [TestMethod]
        public void Retorno_Nao_Logado()
        {
            Negocios.Singleton.UsuarioLogado = null;

            var retorno = NegUsuario.Logar(); ;
            Assert.AreEqual(false, retorno);
        }

        //Acessar constantemente o sistema: Sistema não expirará sessão;
        [TestMethod]
        public void ComAcesso_CincoMinutos()
        {
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { Login = "Bento", Senha = "senha" };
            NegUsuario.Logar();
            Thread.Sleep(1); //Tempo de espera
            var retorno = Negocios.Singleton.comSessao;

            Assert.AreEqual(true, retorno);
        }

        //Ficar sem acessar o sistema por 5 min contínuos: Sistema  expirará sessão e apresentará tela de login;
        [TestMethod]
        public void SemAcesso_CincoMinutos()
        {
            Negocios.Singleton.UsuarioLogado = new Entidades.Usuario() { Login = "Bento", Senha = "senhas" };
            NegUsuario.Logar();
            Thread.Sleep(2000);//Tempo de espera
            var retorno = Negocios.Singleton.comSessao;

            Assert.AreEqual(false, retorno);
        }
    }
}