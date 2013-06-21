using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_ManterUsuario
    {
        Negocios.Diretor NegDiretor = new Negocios.Diretor();

        [TestInitialize]
        public void IniciarTestes()
        {
            this.NegDiretor = new Negocios.Diretor();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {

            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de gerência de usuários: Sistema exibe página para o usuário com permissão.
        [TestMethod]
        public void Permite_Acesso_Manter_Usuario()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Manter_Usuario } });
            var retorno = NegDiretor.TenhoPermissao(Entidades.PermissaoEnum.Manter_Usuario);
            Assert.AreEqual(true, retorno);
        }

        //Acessar opção de gerência de usuários sem permissão: Sistema exibe página de erro para o usuário;        
        [TestMethod]
        public void NaoPermite_Acesso_Manter_Usuario()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });
            var retorno = NegDiretor.TenhoPermissao(Entidades.PermissaoEnum.Manter_Usuario);
            Assert.AreEqual(false, retorno);
        }



        //Preencher dados do novo usuário com erro ou em branco e submeter: sistema exibe mensagem de erro;
        [TestMethod]
        public void Validar_Usuario_Embranco()
        {
            Entidades.Usuario usuario = new Entidades.Usuario();
            usuario.Login = "UFC";
            usuario.Nome = "";
            usuario.Senha = "Silva";
            usuario.TipoUsuario = new Entidades.TipoUsuario() { IdTipoUsuario = 1 };
            var retorno = NegDiretor.TemNomeELogin(usuario);
            Assert.AreEqual(false, retorno);
        }

        //Tentar cadastrar usuário já cadastrado: Apresenta mensagem de erro.
        [TestMethod]
        public void Validar_Usuario_Existente()
        {
            Entidades.Usuario usuario = new Entidades.Usuario();
            usuario.IdUsuario = null;
            usuario.Nome = "Bento Rafael Siqueira";
            var retorno = NegDiretor.TemNomeExistente(usuario);
            Assert.AreEqual(true, retorno);
        }

        //Atribuir permissao por usuario
        [TestMethod]
        public void AssociarPermissaoPorUsuario()
        {
            Entidades.UsuarioPermissao usuarioPermissao = new Entidades.UsuarioPermissao()
            {
                Permissao = new Entidades.Permissao()
                {
                    IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso
                },
                Usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" }
            };

            var retorno = NegDiretor.AssociarPermissaoParaUsuario(usuarioPermissao);

            Assert.AreEqual(true, retorno);
        }

        //Atribuir permissao por tipoUsuario
        [TestMethod]
        public void AssociarPermissaoPorTipoUsuario()
        {
            Entidades.TipoUsuario tipoUsuario = new Entidades.TipoUsuario()
            {
                IdTipoUsuario = (int)Entidades.TipoUsuarioEnum.Reporter
            };

            Entidades.Permissao permissao = new Entidades.Permissao()
            {
                IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso
            };

            var retorno = NegDiretor.AssociarPermissaoParaTipoUsuario(tipoUsuario, permissao);

            Assert.AreEqual(true, retorno);
        }

        //Definir Dias Trabalhados
        [TestMethod]
        public void Definir_Dias_Trabalhados()
        {
            Entidades.DiasTrabalhados diasTrabalhados = new Entidades.DiasTrabalhados();
            diasTrabalhados.Usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };
            diasTrabalhados.DiaSemana = new Entidades.DiaSemana() { IdDia = (int)Entidades.DiaSemanaEnum.Quarta_feira };

            var retorno = NegDiretor.DefinirDiaTrabalhado(diasTrabalhados);

            Assert.AreEqual(true, retorno);
        }

        //Manter_Trabalho
        [TestMethod]
        public void Manter_Trabalho()
        {
            Entidades.Trabalho trabalho = new Entidades.Trabalho();
            trabalho.TipoUsuario = new Entidades.TipoUsuario() { IdTipoUsuario = (int)Entidades.TipoUsuarioEnum.Reporter };
            trabalho.ValorHoraTrabalhada = 999.00M;

            var Ins = NegDiretor.ManterTrabalho(trabalho, Negocios.Singleton.CRUDEnum.INSERIR);
            var Alt = NegDiretor.ManterTrabalho(trabalho, Negocios.Singleton.CRUDEnum.ALTERAR);
            var Del = NegDiretor.ManterTrabalho(trabalho, Negocios.Singleton.CRUDEnum.DELETAR);

            Assert.AreEqual(true, (Ins && Alt && Del));
        }

        //Preencher os dados e submeter: sistema exibe mensagem de sucesso;
        [TestMethod]
        public void Manter_Usuario()
        {
            Entidades.Usuario usuario = new Entidades.Usuario();
            usuario.Login = "UFC";
            usuario.Nome = "Anderson";
            usuario.Senha = "Silva";
            usuario.TipoUsuario = new Entidades.TipoUsuario() { IdTipoUsuario = (int)Entidades.TipoUsuarioEnum.Reporter };
            usuario.UsuarioEndereco = new Entidades.UsuarioEndereco() { Usuario = usuario, Email = "bento@bento", Telefone = "9999" };
            usuario.Contratacao = new Entidades.Contratacao() { Usuario = usuario, DataHora = DateTime.Now };

            var Ins = NegDiretor.ManterUsuario(usuario, Negocios.Singleton.CRUDEnum.INSERIR);
            var Alt = NegDiretor.ManterUsuario(usuario, Negocios.Singleton.CRUDEnum.ALTERAR);
            var Del = NegDiretor.ManterUsuario(usuario, Negocios.Singleton.CRUDEnum.DELETAR);

            Assert.AreEqual(true, (Ins && Alt && Del));
        }

        //Remover Grupo de Trabalho
        [TestMethod]
        public void Remover_Grupo_Trabalho()
        {
            Entidades.GrupoTrabalhoUsuario grupoTrabalhoUsuario = new Entidades.GrupoTrabalhoUsuario();
            grupoTrabalhoUsuario.GrupoTrabalho = new Entidades.GrupoTrabalho() { IdGrupoTrabalho = 1, Descricao = "Grupo 1" };
            grupoTrabalhoUsuario.Usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };
            var retorno = NegDiretor.RemoverGrupoTrabalhoDoUsuario(grupoTrabalhoUsuario);

            Assert.AreEqual(true, retorno);
        }

        //Remover Dia trabalhado
        [TestMethod]
        public void Remover_Dias_Trabalhados()
        {
            Entidades.DiasTrabalhados diasTrabalhados = new Entidades.DiasTrabalhados();
            diasTrabalhados.Usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" };
            diasTrabalhados.DiaSemana = new Entidades.DiaSemana() { IdDia = (int)Entidades.DiaSemanaEnum.Quarta_feira };

            var retorno = NegDiretor.RemoverDiaTrabalhado(diasTrabalhados);

            Assert.AreEqual(true, retorno);
        }

        //Remover Permissao
        [TestMethod]
        public void Remover_Permissao_Usuario()
        {
            Entidades.UsuarioPermissao usuarioPermissao = new Entidades.UsuarioPermissao()
            {
                Permissao = new Entidades.Permissao()
                {
                    IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso
                },
                Usuario = new Entidades.Usuario() { IdUsuario = 1, Nome = "Bento" }
            };

            var retorno = NegDiretor.RemoverPermissaoDoUsuario(usuarioPermissao);

            Assert.AreEqual(true, retorno);
        }
    }
}