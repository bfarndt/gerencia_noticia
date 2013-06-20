using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Noticia.Testes
{
    [TestClass]
    public class UC_SubmeterImagens
    {
        Negocios.Fotografo NegFotografo;
        Negocios.Imagem NegImagem;

        [TestInitialize]
        public void IniciarTestes()
        {
            this.NegImagem = new Negocios.Imagem();
            this.NegFotografo = new Negocios.Fotografo();
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            this.NegImagem = null;
            this.NegFotografo = null;
            Console.WriteLine("Finalizando testes");
        }

        //Acessar opção de Submeter imagens com um usuário que contém a permissão desta:  Apresentar a opção de submeter imagens;
        [TestMethod]
        public void ComAcesso_Para_SubmeterImagens()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Submeter_Imagens } });

            var retorno = NegFotografo.TenhoPermissao(Entidades.PermissaoEnum.Submeter_Imagens);

            Assert.AreEqual(true, retorno);
        }

        //Acessar opção de Submeter imagens com um usuário que não contém a permissão desta:  Apresentar uma mensagem de acesso negado ao usuário;
        [TestMethod]
        public void SemAcesso_Para_SubmeterImagens()
        {
            Negocios.Singleton.UsuarioPermissoes = new List<Entidades.UsuarioPermissao>();
            Negocios.Singleton.UsuarioPermissoes.Add(new Entidades.UsuarioPermissao() { Permissao = new Entidades.Permissao() { IdPermissao = (int)Entidades.PermissaoEnum.Efetuar_Acesso } });

            var retorno = NegFotografo.TenhoPermissao(Entidades.PermissaoEnum.Submeter_Imagens);

            Assert.AreEqual(false, retorno);
        }

        //Efetuar upload de imagem com extensões jpg, jpeg e png: Sistema efetua o mesmo e apresenta mensagem de sucesso após término;
        [TestMethod]
        public void Efetuar_Upload_Com_Extensao_Correta()
        {
            FileInfo file = new FileInfo(@"TesteImagens\Pequena.jpg");
            var retorno = NegImagem.ValidarExtensao(file);
            Assert.AreEqual(true, retorno);

        }

        //Efetuar upload de arquivos diferentes de jpg, jpeg e png: Apresentar mensagem de negação;
        [TestMethod]
        public void Efetuar_Upload_Com_Extensao_Incorreta()
        {
            FileInfo file = new FileInfo(@"TesteImagens\ExtensaoDiferente.bmp");
            var retorno = NegImagem.ValidarExtensao(file);
            Assert.AreEqual(false, retorno);
        }

        //Efetuar upload de arquivos com menos de 2 mb: Sistema efetua o mesmo e exibe mensagem de sucesso
        [TestMethod]
        public void Efetuar_Upload_Com_Menos_2MB()
        {
            //\Noticias\Noticia.Testes\bin\Debug\TesteImagens
            FileInfo file = new FileInfo(@"TesteImagens\Pequena.jpg");
            var retorno = NegImagem.ValidarTamanho(file);
            Assert.AreEqual(true, retorno);
        }

        //Efetuar upload de arquivos com mais de 2 mb: Sistema apresenta mensagem de erro.
        [TestMethod]
        public void Efetuar_Upload_Com_Mais_2MB()
        {
            //\Noticias\Noticia.Testes\bin\Debug\TesteImagens
            FileInfo file = new FileInfo(@"TesteImagens\Grande.jpg");
            var retorno = NegImagem.ValidarTamanho(file);
            Assert.AreEqual(false, retorno);
        }

        //SubmeterImagem
        [TestMethod]
        public void Submeter_Imagem()
        {
            FileInfo file = new FileInfo(@"TesteImagens\Pequena.jpg");
            var retorno = NegFotografo.SubmeterImagem(file);
            Assert.AreEqual(true, retorno);
        }
    }
}
