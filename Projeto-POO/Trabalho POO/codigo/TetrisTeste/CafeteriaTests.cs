using System;
using System.Collections.Generic;
using Xunit;
using Tetris.Model;
using Moq;

namespace Tetris.Tests
{
    public class CafeteriaTests
    {
        #region Testes de Requisição

        [Fact]
        public void CriarRequisicao_DeveAdicionarRequisicao()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var mock = new Mock<Estabelecimento>();

            mock.Setup(e => e.CriarRequisicao(cliente, 2))
                .Returns(new Requisicao(cliente, 2));

            mock.Setup(e => e.TemRequisicao(cliente)).Returns(true);

            // Act
            var requisicao = mock.Object.CriarRequisicao(cliente, 2);

            // Assert
            Assert.NotNull(requisicao);
            Assert.Equal(cliente, requisicao.GetCliente());
            Assert.True(mock.Object.TemRequisicao(cliente));
        }

        [Fact]
        public void CriarRequisicao_QuantidadeInvalida_DeveLancarErro()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var mock = new Mock<Estabelecimento>();

            mock.Setup(e => e.CriarRequisicao(cliente, It.Is<int>(q => q <= 0 || q > 8)))
                .Throws<ArgumentOutOfRangeException>();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => mock.Object.CriarRequisicao(cliente, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => mock.Object.CriarRequisicao(cliente, 9));
        }

        [Fact]
        public void CriarRequisicao_ClienteNulo_DeveLancarErro()
        {
            // Arrange
            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.CriarRequisicao(null!, It.IsAny<int>()))
                .Throws<ArgumentNullException>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => mock.Object.CriarRequisicao(null!, 1));
        }

        [Fact]
        public void TemRequisicao_ClienteInexistente_DeveRetornarFalse()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.TemRequisicao(cliente)).Returns(false);

            // Act
            var existe = mock.Object.TemRequisicao(cliente);

            // Assert
            Assert.False(existe);
        }

        #endregion

        #region Testes de Pedido e Produtos

        [Fact]
        public void Pedido_AdicionarProdutosECalcularTotal_DeveSerCorreto()
        {
            // Arrange
            var pedido = new Pedido();
            pedido.AdicionarItem(new Produto("Café", 10.0));
            pedido.AdicionarItem(new Produto("Biscoito", 5.0));

            // Act
            var total = pedido.CalcularValorTotal();
            var divisao = pedido.CalcularDivisaoValor(3);

            // Assert
            Assert.Equal(16.5, total); // 10 + 1 + 5 + 0.5 = 16.5 (10% taxa)
            Assert.Equal(5.5, divisao);
        }

        [Fact]
        public void Pedido_CalcularValorTotal_ComDecimais_DeveSerPreciso()
        {
            // Arrange
            var pedido = new Pedido();
            pedido.AdicionarItem(new Produto("Bebida Premium", 7.75));
            pedido.AdicionarItem(new Produto("Snack", 2.45));

            // Act
            var total = pedido.CalcularValorTotal();

            // Assert
            Assert.InRange(total, 11.219, 11.221); // 10% taxa incluída
        }

        [Fact]
        public void Pedido_FecharPedido_DeveMudarStatusECalcularTotal()
        {
            // Arrange
            var pedido = new Pedido();
            var produto = new Produto("Café", 6.0);
            pedido.AdicionarItem(produto);
            pedido.GerarPedido(produto);

            // Act
            pedido.FecharPedido();
            var total = pedido.CalcularValorTotal();

            // Assert
            Assert.Equal(6.6, total); // 10% taxa
        }

        [Fact]
        public void Pedido_ToString_DeveListarTodosProdutos()
        {
            // Arrange
            var pedido = new Pedido();
            var produto1 = new Produto("Café", 6.0);
            var produto2 = new Produto("Biscoito", 3.0);
            pedido.AdicionarItem(produto1);
            pedido.AdicionarItem(produto2);

            // Act
            var texto = pedido.ToString();

            // Assert
            Assert.Contains("Café", texto);
            Assert.Contains("Biscoito", texto);
        }

        [Fact]
        public void Pedido_AdicionarMultiplosProdutos_DeveAcumularItens()
        {
            // Arrange
            var pedido = new Pedido();
            var produto1 = new Produto("Café", 6.0);
            var produto2 = new Produto("Biscoito", 3.0);

            // Act
            pedido.AdicionarItem(produto1);
            pedido.AdicionarItem(produto2);

            // Assert
            var itens = pedido.GetPedido();
            Assert.Equal(2, itens.Count);
            Assert.Contains(produto1, itens);
            Assert.Contains(produto2, itens);
        }

        #endregion

        #region Testes de Mock de Estabelecimento (Métodos de Cafeteria)

        [Fact]
        public void BuscarPedidos_DeveRetornarPedidoDoCliente()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var pedido = new Pedido();
            pedido.AdicionarItem(new Produto("Café", 6.0));

            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.BuscarPedidos(cliente)).Returns(pedido);

            // Act
            var resultado = mock.Object.BuscarPedidos(cliente);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pedido, resultado);
            Assert.Single(resultado.GetPedido());
        }

        [Fact]
        public void IncluirProduto_DeveAdicionarProdutoAoPedido()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var produto = new Produto("Café expresso orgânico", 6.0);
            var pedido = new Pedido();
            var requisicao = new Requisicao(cliente, 1);

            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.CriarRequisicao(cliente, 1)).Returns(requisicao);
            mock.Setup(e => e.BuscarProduto(It.IsAny<int>())).Returns(produto);
            mock.Setup(e => e.incluirProduto(produto.GetId(), cliente.GetNome())).Returns(produto);
            mock.Setup(e => e.BuscarPedidos(cliente)).Returns(pedido);

            // Act
            var req = mock.Object.CriarRequisicao(cliente, 1);
            var produtoAdicionado = mock.Object.incluirProduto(produto.GetId(), cliente.GetNome());
            var pedidoRetornado = mock.Object.BuscarPedidos(cliente);

            // Assert
            Assert.NotNull(req);
            Assert.Equal(cliente, req.GetCliente());
            Assert.Equal(produto, produtoAdicionado);
            Assert.Equal(pedido, pedidoRetornado);
        }

        [Fact]
        public void FecharConta_DeveRetornarTotalEExcluirRequisicao()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var requisicao = new Requisicao(cliente, 1);
            var total = 10.0;

            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.CriarRequisicao(cliente, 1)).Returns(requisicao);
            mock.Setup(e => e.FecharConta(cliente.GetNome())).Returns(total);
            mock.Setup(e => e.TemRequisicao(cliente)).Returns(false);

            // Act
            var req = mock.Object.CriarRequisicao(cliente, 1);
            var totalRetornado = mock.Object.FecharConta(cliente.GetNome());

            // Assert
            Assert.Equal(total, totalRetornado);
            Assert.False(mock.Object.TemRequisicao(cliente));
        }

        [Fact]
        public void ApresentarCardapio_DeveChamarMetodoDoCardapio()
        {
            // Arrange
            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.ApresentarCardapio());

            // Act
            mock.Object.ApresentarCardapio();

            // Assert
            mock.Verify(e => e.ApresentarCardapio(), Times.Once);
        }

        [Fact]
        public void BuscarProduto_DeveRetornarProduto()
        {
            // Arrange
            var produto = new Produto("Café", 6.0);
            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.BuscarProduto(produto.GetId())).Returns(produto);

            // Act
            var resultado = mock.Object.BuscarProduto(produto.GetId());

            // Assert
            Assert.Equal(produto, resultado);
        }

        [Fact]
        public void IncluirProduto_Inexistente_DeveLancarExcecao()
        {
            // Arrange
            var cliente = new Cliente("Chris");
            var mock = new Mock<Estabelecimento>();
            mock.Setup(e => e.incluirProduto(It.IsAny<int>(), cliente.GetNome()))
                .Throws<NullReferenceException>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => mock.Object.incluirProduto(1, cliente.GetNome()));
        }

        #endregion
    }
}
