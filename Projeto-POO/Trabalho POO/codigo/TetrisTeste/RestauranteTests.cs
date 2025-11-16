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
            var mock = new Mock<Restaurante>() { CallBase = true };

            var requisicaoEsperada = new Requisicao(cliente, 2);

            var requisicao = mock.Object.CriarRequisicao(cliente, 2);
            var existe = mock.Object.TemRequisicao(cliente);

            Assert.NotNull(requisicao);
            Assert.Equal(cliente, requisicao.GetCliente());
            Assert.True(existe);
        }

        [Fact]
        public void IncluirProduto_ViaAbstracao_DeveRetornarProdutoMockado()
        {
            new Produto("Reset", 1).reset();
            var cliente = new Cliente("Maria");
            var mock = new Mock<Restaurante>() { CallBase = true };

            var produto = mock.Object.BuscarProduto(6);

            var requisicao = new Requisicao(cliente, 4);

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 4);
            var produtoAdicionado = mock.Object.incluirProduto(produto.GetId(), cliente.GetNome());

            Assert.Equal(produto.GetId(), produtoAdicionado.GetId());
        }

        [Fact]
        public void FecharConta_ViaAbstracao_DeveRetornarValorMockado()
        {
            new Produto("Reset", 1).reset();
            var cliente = new Cliente("Lucas");
            var mock = new Mock<Restaurante>() { CallBase = true };

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 4);
            requisicaoCriada.ReceberProduto(mock.Object.BuscarProduto(4));
            requisicaoCriada.ReceberProduto(mock.Object.BuscarProduto(7));
            var total = mock.Object.FecharConta(cliente.GetNome());
            var existe = mock.Object.TemRequisicao(cliente);

            Assert.Equal(23.1, total);
            Assert.False(existe);
        }

        [Fact]
        public void BuscarPedidos_ViaAbstracao_DeveRetornarPedidoMockado()
        {
            new Produto("Reset", 1).reset();
            var cliente = new Cliente("Ana");
            var mock = new Mock<Restaurante>() { CallBase = true };

            var requisicao = new Requisicao(cliente, 4);
            var pedido = requisicao.GetPedido();
            pedido.AdicionarItem(new Produto("Café", 5.0));

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 4);
            mock.Object.incluirProduto(2, "Ana");
            var pedidoRetornado = mock.Object.BuscarPedidos(cliente);

            Assert.NotNull(pedidoRetornado);
            Assert.Single(pedidoRetornado.GetPedido());
        }

        [Fact]
        public void TemRequisicao_ViaAbstracao_ClienteExistente_DeveRetornarTrue()
        {
            var cliente = new Cliente("Julia");
            var mock = new Mock<Restaurante>() { CallBase = true };

            var requisicao = new Requisicao(cliente, 8);

            var requisicaoCriada = mock.Object.CriarRequisicao(cliente, 8);
            var existe = mock.Object.TemRequisicao(cliente);

            Assert.True(existe);
        }

        [Fact]
        public void IncluirProduto_ClienteNaoExistente_DeveLancarExcecao()
        {
            var cliente = new Cliente("Pedro");
            var mock = new Mock<Restaurante>() { CallBase = true };

            Assert.Throws<NullReferenceException>(() =>
                mock.Object.incluirProduto(1, cliente.GetNome()));
        }

        [Fact]
        public void FecharConta_ViaAbstracao_DeveRetornarValor()
        {
            new Produto("Reset", 1).reset();
            var cliente = new Cliente("Carlos");
            var mock = new Mock<Restaurante>() { CallBase = true };

            var requisicao = mock.Object.CriarRequisicao(cliente, 4);
            var produto = mock.Object.BuscarProduto(3);
            requisicao.ReceberProduto(produto);

            double total = mock.Object.FecharConta(cliente.GetNome());
            bool existe = mock.Object.TemRequisicao(cliente);

            Assert.Equal(27.5, total);
            Assert.False(existe);
        }

        [Fact]
        public void CriarRequisicao_AlocaMesa_ViaAbstracao()
        {
            var cliente = new Cliente("Paula");
            var requisicao = new Requisicao(cliente, 4);

            var mock = new Mock<Restaurante>() { CallBase = true };

            var r = mock.Object.CriarRequisicao(cliente, 4);
            Assert.NotNull(r);
            Assert.Equal(cliente, r.GetCliente());
            Assert.True(mock.Object.TemRequisicao(cliente));
        }

        [Fact]
        public void BuscarPedidos_ClienteNaoExistente_DeveLancarExcecao()
        {
            var cliente = new Cliente("NaoExiste");
            var mock = new Mock<Restaurante>() { CallBase = true };

            Assert.Throws<NullReferenceException>(() =>
                mock.Object.BuscarPedidos(cliente));
        }

        [Fact]
        public void FecharConta_ClienteNaoExistente_DeveLancarExcecao()
        {
            var mock = new Mock<Restaurante>() { CallBase = true };

            Assert.Throws<NullReferenceException>(() =>
                mock.Object.FecharConta("Desconhecido"));
        }
    }
}