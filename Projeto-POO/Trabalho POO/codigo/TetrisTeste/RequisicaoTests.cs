using Xunit;
using Moq;
using Tetris.Model; 
using System;

namespace Tetris.Tests
{
    public class RequisicaoTests
    {
        [Fact]
        public void Construtor_DeveInicializarValoresCorretamente()
        {
            int quantidadePessoas = 2;
            var cliente = new Cliente("Chris");

            var requisicao = new Requisicao(cliente, quantidadePessoas);

            Assert.NotNull(requisicao.GetPedido());
            Assert.Equal(cliente, requisicao.GetCliente());
            Assert.Equal(quantidadePessoas, requisicao.GetQtdPessoas());
        }

        [Fact]
        public void AlocarMesa_DeveAtribuirEMesaCorretamente()
        {
            var cliente = new Cliente("Chris");
            var requisicao = new Requisicao(cliente, 1);

            var mockMesa = new Mock<Mesa>(4);

            var mesaAlocada = requisicao.AlocarMesa(mockMesa.Object);

            Assert.Equal(mockMesa.Object, requisicao.GetMesa());
            Assert.Equal(mockMesa.Object, mesaAlocada);
        }

        [Fact]
        public void BuscarCliente_ComNomeCorreto_DeveRetornarTrue()
        {
            string nomeCliente = "Chris";
            var clienteReal = new Cliente(nomeCliente);
            var requisicao = new Requisicao(clienteReal, 1);

            bool resultado = requisicao.BuscarCliente(nomeCliente);

            Assert.True(resultado);
        }

        [Fact]
        public void BuscarCliente_ComNomeErrado_DeveRetornarFalse()
        {
            var clienteReal = new Cliente("Chris");
            var requisicao = new Requisicao(clienteReal, 1);

            bool resultado = requisicao.BuscarCliente("NomeErrado");

            Assert.False(resultado);
        }

        [Fact]
        public void FecharConta_DeveRetornarValorTotalDoPedidoComTaxa()
        {
            var cliente = new Cliente("Chris");
            var requisicao = new Requisicao(cliente, 2);
            var produto1 = new Produto("Suco", 20.00);
            var produto2 = new Produto("Lanche", 30.00);

            requisicao.ReceberProduto(produto1);
            requisicao.ReceberProduto(produto2);
            double valorTotal = requisicao.fecharConta();

            Assert.Equal(55.00, valorTotal);
        }

        [Fact]
        public void FecharConta_PedidoVazio_DeveRetornarZero()
        {
            var cliente = new Cliente("Chris");
            var requisicao = new Requisicao(cliente, 1);

            double valorTotal = requisicao.fecharConta();

            Assert.Equal(0, valorTotal);
        }


        [Fact]
        public void EncerrarRequisicao_DeveRegistrarSaida()
        {
            var cliente = new Cliente("Chris");
            var requisicao = new Requisicao(cliente, 1);
            System.Threading.Thread.Sleep(10);

            DateTime saida = requisicao.EncerrarRequisicao();

            Assert.True(saida <= DateTime.Now);
        }

        [Fact]
        public void ToString_SemMesa_DeveFormatarStringCorretamente()
        {
            var mockCliente = new Mock<Cliente>("Chris");
            mockCliente.Setup(c => c.ToString()).Returns("Cliente: Chris");

            var requisicao = new Requisicao(mockCliente.Object, 2);

            string resultado = requisicao.ToString();

            Assert.Contains("Cliente: Chris", resultado);
            Assert.Contains("Total do pedido: 0", resultado);
            Assert.DoesNotContain("Mesa ocupada:", resultado);
        }

        [Fact]
        public void ToString_ComMesa_DeveIncluirInfoDaMesa()
        {
            var mockCliente = new Mock<Cliente>("Chris");
            mockCliente.Setup(c => c.ToString()).Returns("Cliente: Chris");

            var mockMesa = new Mock<Mesa>(2);

            mockMesa.Setup(m => m.GetId()).Returns(5);

            var requisicao = new Requisicao(mockCliente.Object, 2);
            requisicao.AlocarMesa(mockMesa.Object);

            string resultado = requisicao.ToString();

            Assert.Contains("Cliente: Chris", resultado);
            Assert.Contains("Total do pedido: 0", resultado);
            Assert.Contains("Mesa ocupada: mesa 5", resultado);
        }
    }
}