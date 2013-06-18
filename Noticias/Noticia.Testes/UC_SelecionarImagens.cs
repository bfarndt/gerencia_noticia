using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_SelecionarImagens
    {
        Negocios.Reporter NegReporter;
        Negocios.Imagem NegImagem;

        [TestInitialize]
        public void IniciarTestes()
        {
            NegReporter = new Negocios.Reporter();
            NegImagem = new Negocios.Imagem();
            Negocios.Sessao.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Login = "Bento", Senha = "senha" };
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Visualizar Notícias que contém botão editar disponível com um usuário que contém a permissão desta:  Apresentar lista de notícias;
        [TestMethod]
        public void ComAcesso_Para_SelecionarImagens()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Selecionar_Imagens } });

            var retorno = NegReporter.TenhoPermissao(Entidades.PermissaoEnum.Selecionar_Imagens);

            Assert.AreEqual(true, retorno);
        }

        //Acessar opção de Visualizar Notícias que contém botão editar disponível  com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Para_SelecionarImagens()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegReporter.TenhoPermissao(Entidades.PermissaoEnum.Selecionar_Imagens);

            Assert.AreEqual(false, retorno);
        }

        //Selecionar imagens, adicionar legenda e submeter edição: sistema apresenta mensagem de sucesso.
        //(Ele também visualiza as noticias atraves dos grupos, deverá aparecer para ele as imagens que foram associadas)
        [TestMethod]
        public void Validar_Selecao_Imagem()
        {
            Entidades.Imagem imagem = new Entidades.Imagem() { Legenda = "Imagem do Morumbi" };
            var retorno = NegImagem.ValidarImagem(imagem);
            Assert.AreEqual(true, retorno);
        }

        //Selecionar imagem e não adicionar legenda: sistema apresenta mensagem de erro.
        [TestMethod]
        public void NaoValidar_Selecao_Imagem()
        {
            Entidades.Imagem imagem = new Entidades.Imagem() { Legenda = "" };
            var retorno = NegImagem.ValidarImagem(imagem);
            Assert.AreEqual(false, retorno);
        }

        //Visualizar Imagens de notícias associadas pelo usuário logado
        [TestMethod]
        public void Visualizar_Imagem_Associadas()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Selecionar_Imagens } });

            var retorno = NegImagem.ImagensDeNoticiasAssociadas();
            Assert.IsNotNull(retorno);
        }

        //Selecionar Imagem
        [TestMethod]
        public void Selecionar_Imagem()
        {
            Entidades.Imagem imagem = new Entidades.Imagem();
            imagem.IdImagem = 2;
            imagem.Legenda = "São Paulo";

            var retorno = NegReporter.SelecionarImagem(imagem);

            Assert.AreEqual(true, retorno);
        }
    }
}
