using System;
using Xunit;
using Tetris.Model;

namespace Tetris.Tests
{
    public class PedidosTests
    {
        [Fact]
        public void AdicionarItem_DeveAdicionarProdutoNaLista()
        {
            var pedido = new Pedido();
            var produto = new Produto("Café", 5.0);

            pedido.AdicionarItem(produto);

            Assert.Contains(produto, pedido.GetPedido());
            Assert.Single(pedido.GetPedido());
        }

        [Fact]
        public void CalcularValorTotal_DeveIncluirTaxaServico()
        {
            var pedido = new Pedido();
            pedido.AdicionarItem(new Produto("Lanche", 10.0)); 
            pedido.AdicionarItem(new Produto("Suco", 20.0));   

            var total = pedido.CalcularValorTotal();

            Assert.Equal(33.0, total);
        }

        [Fact]
        public void CalcularDivisaoValor_DeveDividirCorretamente()
        {
            var pedido = new Pedido();
            pedido.AdicionarItem(new Produto("Pizza", 50.0)); 

            var valorPorPessoa = pedido.CalcularDivisaoValor(5);

            Assert.Equal(11.0, valorPorPessoa);
        }

        [Fact]
        public void ToString_DeveListarProdutos()
        {
            var pedido = new Pedido();
            var produto = new Produto("Água", 3.0);
            pedido.AdicionarItem(produto);

            var texto = pedido.ToString();

            Assert.Contains("Água", texto);
            Assert.Contains("3", texto);
        }
    }
}