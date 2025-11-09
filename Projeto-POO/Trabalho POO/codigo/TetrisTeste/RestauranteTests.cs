using System;
using Xunit;
using Moq;
using Tetris.Model;

namespace Tetris.Tests
{
    public class RestauranteTests
    {
        [Fact]
        public void CriarRequisicao_ViaAbstracao_DeveRetornarRequisicaoMockada()
        {
            var cliente = new Cliente("Chris");
            var mock = new Mock<Estabelecimento>();

            var requisicaoEsperada = new Requisicao(cliente, 2);
            mock.Setup(e => e.CriarRequisicao(cliente, 2))
                .Returns(requisicaoEsperada);
            mock.Setup(e => e.TemRequisicao(cliente))
                .Returns(true);

            var requisicao = mock.Object.CriarRequisicao(cliente, 2);
            var existe = mock.Object.TemRequisicao(cliente);

            Assert.NotNull(requisicao);
            Assert.Equal(cliente, requisicao.GetCliente());
            Assert.True(existe);
        }

        [Fact]
        public void IncluirProduto_ViaAbstracao_DeveRetornarProdutoMockado()
        {
            var cliente = new Cliente("Maria");
            var produto = new Produto("Sobremesa", 6.0);
            var mock = new Mock<Estabelecimento>();

            var requisicao = new Requisicao(cliente, 4);
            mock.Setup(e => e.CriarRequisicao(cliente, 4)).Returns(requisicao);
            mock.Setup(e => e.incluirProduto(produto.GetId(), cliente.GetNome()))
                .Returns(produto);

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 4);
            var produtoAdicionado = mock.Object.incluirProduto(produto.GetId(), cliente.GetNome());

            Assert.Equal(produto.GetId(), produtoAdicionado.GetId());
        }

        [Fact]
        public void FecharConta_ViaAbstracao_DeveRetornarValorMockado()
        {
            var cliente = new Cliente("Lucas");
            var mock = new Mock<Estabelecimento>();

            var requisicao = new Requisicao(cliente, 4);
            requisicao.ReceberProduto(new Produto("Prato Principal", 20.0));
            requisicao.ReceberProduto(new Produto("Bebida", 5.0));

            mock.Setup(e => e.CriarRequisicao(cliente, 4)).Returns(requisicao);
            mock.Setup(e => e.FecharConta(cliente.GetNome())).Returns(27.5);
            mock.Setup(e => e.TemRequisicao(cliente)).Returns(false);

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 4);
            var total = mock.Object.FecharConta(cliente.GetNome());
            var existe = mock.Object.TemRequisicao(cliente);

            Assert.Equal(27.5, total);
            Assert.False(existe);
        }

        [Fact]
        public void BuscarPedidos_ViaAbstracao_DeveRetornarPedidoMockado()
        {
            // Arrange
            var cliente = new Cliente("Ana");
            var mock = new Mock<Estabelecimento>();

            var requisicao = new Requisicao(cliente, 4);
            var pedido = requisicao.GetPedido();
            pedido.AdicionarItem(new Produto("Café", 5.0));

            mock.Setup(e => e.CriarRequisicao(cliente, 4)).Returns(requisicao);
            mock.Setup(e => e.BuscarPedidos(cliente)).Returns(pedido);

            // Act
            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 4);
            var pedidoRetornado = mock.Object.BuscarPedidos(cliente);

            // Assert
            Assert.NotNull(pedidoRetornado);
            Assert.Single(pedidoRetornado.GetPedido());
        }


        [Fact]
        public void TemRequisicao_ViaAbstracao_ClienteExistente_DeveRetornarTrue()
        {
            var cliente = new Cliente("Julia");
            var mock = new Mock<Estabelecimento>();

            var requisicao = new Requisicao(cliente, 8);
            mock.Setup(e => e.CriarRequisicao(cliente, 8)).Returns(requisicao);
            mock.Setup(e => e.TemRequisicao(cliente)).Returns(true);

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 8);
            var existe = mock.Object.TemRequisicao(cliente);

            Assert.True(existe);
        }

        [Fact]
        public void IncluirProduto_ClienteNaoExistente_DeveLancarExcecao()
        {
            var cliente = new Cliente("Pedro");
            var mock = new Mock<Estabelecimento>();

            mock.Setup(e => e.incluirProduto(It.IsAny<int>(), cliente.GetNome()))
                .Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() =>
                mock.Object.incluirProduto(1, cliente.GetNome()));
        }

        [Fact]
        public void FecharConta_ViaAbstracao_DeveRetornarValor()
        {
            var cliente = new Cliente("Carlos");
            var mock = new Mock<Estabelecimento>();

            var requisicao = new Requisicao(cliente, 4);
            requisicao.ReceberProduto(new Produto("Prato", 15.0));

            mock.Setup(e => e.CriarRequisicao(cliente, 4)).Returns(requisicao);
            mock.Setup(e => e.FecharConta(cliente.GetNome())).Returns(15.0);
            mock.Setup(e => e.TemRequisicao(cliente)).Returns(false);

            double total = mock.Object.FecharConta(cliente.GetNome());
            bool existe = mock.Object.TemRequisicao(cliente);

            Assert.Equal(15.0, total);
            Assert.False(existe);
        }

        [Fact]
        public void CriarRequisicao_AlocaMesa_ViaAbstracao()
        {
            var cliente = new Cliente("Paula");
            var requisicao = new Requisicao(cliente, 4);

            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.CriarRequisicao(cliente, 4)).Returns(requisicao);
            mock.Setup(e => e.TemRequisicao(cliente)).Returns(true);

            var r = mock.Object.CriarRequisicao(cliente, 4);
            Assert.NotNull(r);
            Assert.Equal(cliente, r.GetCliente());
            Assert.True(mock.Object.TemRequisicao(cliente));
        }

        [Fact]
        public void BuscarPedidos_ClienteNaoExistente_DeveLancarExcecao()
        {
            var cliente = new Cliente("NaoExiste");
            var mock = new Mock<Estabelecimento>();

            mock.Setup(e => e.BuscarPedidos(cliente))
                .Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() =>
                mock.Object.BuscarPedidos(cliente));
        }


        [Fact]
        public void FecharConta_ClienteNaoExistente_DeveLancarExcecao()
        {
            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.FecharConta("Desconhecido"))
                .Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() =>
                mock.Object.FecharConta("Desconhecido"));
        }

    }
}
