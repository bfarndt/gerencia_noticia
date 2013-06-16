﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_CriarNoticia
    {
        Negocios.Noticia NegNoticia;
        Negocios.Usuario NegUsuario;

        [TestInitialize]
        public void IniciarTestes()
        {
            Negocios.Sessao.IniciarSessao();
            Negocios.Sessao.UsuarioLogado = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };
            NegNoticia = new Negocios.Noticia();
            NegUsuario = new Negocios.Usuario();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            NegNoticia = null;
            NegUsuario = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de criar notícia com um usuário que contém a permissão desta:  Apresentar a opção de criar notícias;
        [TestMethod]
        public void Permite_Acesso_Noticia()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Criar_Noticia } });
            var retorno = NegUsuario.TemPermissao(Entidades.PermissaoEnum.Criar_Noticia);
            Assert.AreEqual(true, retorno);
        }

        //Acessar opção de criar notícia com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void NaoPermite_Acesso_Noticia()
        {
            Negocios.Sessao.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Sessao.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });
            var retorno = NegUsuario.TemPermissao(Entidades.PermissaoEnum.Criar_Noticia);
            Assert.AreEqual(false, retorno);
        }

        //Informar os campos Título e conteúdo da notícia confirmando o cadastrado dos mesmos: Aprensentar mensagem de confirmação do cadastro;
        [TestMethod]
        public void Validar_Inclusao_Noticia()
        {
            Negocios.Sessao.NoticiaAtual = new Entidades.Noticia() { Titulo = "São Paulo", Conteudo = "Melhor time do Brasil" };
            var retorno = NegNoticia.ValidarCampos();
            Assert.AreEqual(true, retorno);
        }

        //Não informar os campos Título ou conteúdo da notícia confirmando o cadastro dos mesmos: Apresentar mensagem de aviso da falta de preenchimento;
        [TestMethod]
        public void NaoValidar_Inclusao_Noticia()
        {
            Negocios.Sessao.NoticiaAtual = new Entidades.Noticia() { Titulo = "", Conteudo = "Melhor time do Brasil" };
            var retorno = NegNoticia.ValidarCampos();
            Assert.AreEqual(false, retorno);
        }

        //Finalizar cadastro clicando no botão confirmar: Cadastro finalizado e notícia é cadastrada
        [TestMethod]
        public void Finalizar_Cadastro_Noticia()
        {
            Negocios.Sessao.NoticiaAtual = new Entidades.Noticia() { Titulo = "Microsoft Vs Apple", Conteudo = "Popularidade, qualidade e custo" };
            var retorno = NegNoticia.CriarNoticia();
            Assert.AreEqual(true, retorno);
        }

        //Falha durante o processo: Não gravar notícia e voltar a tela principal;
        [TestMethod]
        public void Falha_Cadastro_Noticia()
        {
            Negocios.Sessao.NoticiaAtual = null;
            var retorno = NegNoticia.CriarNoticia();
            Assert.AreEqual(false, retorno);
        }

        //Associar um grupo de trabalho a esta notícia: Sistema deverá atribuir aos grupos de trabalhos e seus repectivos usuários.
        [TestMethod]
        public void AssociarGrupoTrabalho()
        {
            Negocios.Sessao.NoticiaAtual = new Entidades.Noticia() { Titulo = "Microsoft Vs Apple", Conteudo = "Popularidade, qualidade e custo" };
            NegNoticia.CriarNoticia();
            Entidades.GrupoTrabalho grupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = 1, Descricao = "Grupo 1" };
            var retorno = NegNoticia.AssociarGrupoTrabalho(grupoTrabalho);

            Assert.AreEqual(true, retorno);
        }
    }
}
