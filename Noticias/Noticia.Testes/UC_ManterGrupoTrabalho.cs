using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_ManterGrupoTrabalho
    {
        Negocios.Usuario NegUsuario;
        Negocios.GrupoTrabalho NegGrupoTrabalho;
        Negocios.Diretor NegDiretor;

        [TestInitialize]
        public void IniciarTestes()
        {
            this.NegUsuario = new Negocios.Usuario();
            this.NegGrupoTrabalho = new Negocios.GrupoTrabalho();
            this.NegDiretor = new Negocios.Diretor();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            this.NegUsuario = null;
            this.NegGrupoTrabalho = null;
            this.NegDiretor = null;

            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Gerenciar Grupo de Trabalho com um usuário que contém a permissão desta:  Apresentar a opção de Gerenciar Grupo de Trabalho;
        [TestMethod]
        public void Permite_Acesso_Manter_GrupoTrabalho()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Manter_grupo_de_trabalho } });
            var retorno = NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Manter_grupo_de_trabalho);
            Assert.AreEqual(true, retorno);
        }

        //Acessar opção de Gerenciar Grupo de Trabalho com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void NaoPermite_Acesso_Manter_GrupoTrabalho()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });
            var retorno = NegUsuario.TenhoPermissao(Entidades.PermissaoEnum.Manter_grupo_de_trabalho);
            Assert.AreEqual(false, retorno);
        }

        //Informar o campo de Descrição do Grupo de trabalho confirmando o cadastrado do mesmo: Aprensentar mensagem de confirmação do cadastro;
        [TestMethod]
        public void Validar_Inclusao_GrupoTrabalho()
        {
            Entidades.GrupoTrabalho grupoTrabalho = new Entidades.GrupoTrabalho() { Descricao = "Grupo do São Paulo" };
            var retorno = NegGrupoTrabalho.TemGrupoTrabalhoEmBranco(grupoTrabalho);
            Assert.AreEqual(false, retorno);
        }

        //Não informar o campo de Descrição do Grupo de trabalho confirmando o cadastro do mesmo: Apresentar mensagem de aviso da falta de preenchimento;
        [TestMethod]
        public void NaoValidar_Inclusao_GrupoTrabalho()
        {
            Entidades.GrupoTrabalho grupoTrabalho = new Entidades.GrupoTrabalho() { Descricao = "" };
            var retorno = NegGrupoTrabalho.TemGrupoTrabalhoEmBranco(grupoTrabalho);
            Assert.AreEqual(true, retorno);
        }

        //Efetuar atribuição dos respectivos usuários ao grupo confirmando atribuição: Salvar atribuições Apresentar mensagem de confirmação;
        [TestMethod]
        public void AssociarUsuarioAoGrupoTrabalho()
        {
            Entidades.GrupoTrabalhoUsuario grupoTrabalhoUsuario = new Entidades.GrupoTrabalhoUsuario();
            grupoTrabalhoUsuario.GrupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = 1, Descricao = "Grupo 1" };
            grupoTrabalhoUsuario.Usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };

            var retorno = NegDiretor.AssociarGrupoTrabalhoParaUsuario(grupoTrabalhoUsuario);

            Assert.AreEqual(true, retorno);
        }

        //Efetuar alteração de um grupo com descrição já cadastrada: Apresentar mensagem de já existente e solicitar preencher novamente
        [TestMethod]
        public void Validar_Inclusao_GrupoTrabalho_Diferente_Descricao()
        {
            Entidades.GrupoTrabalho grupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = 1, Descricao = "Grupo São Paulo" };
            Entidades.Usuario usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };
            NegDiretor.AssociarGrupoTrabalhoParaUsuario(new Entidades.GrupoTrabalhoUsuario() { GrupoTrabalho = grupoTrabalho, Usuario = usuario });
            var retorno = NegGrupoTrabalho.TemGrupoTrabalhoExistente(grupoTrabalho);
            Assert.AreEqual(false, retorno);
        }

        [TestMethod]
        public void Validar_Inclusao_GrupoTrabalho_Mesma_Descricao()
        {
            Entidades.GrupoTrabalho grupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = 1, Descricao = "Grupo 1" };
            Entidades.Usuario usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };
            NegDiretor.AssociarGrupoTrabalhoParaUsuario(new Entidades.GrupoTrabalhoUsuario() { GrupoTrabalho = grupoTrabalho, Usuario = usuario });
            var retorno = NegGrupoTrabalho.TemGrupoTrabalhoExistente(grupoTrabalho);
            Assert.AreEqual(true, retorno);
        }

    }
}
