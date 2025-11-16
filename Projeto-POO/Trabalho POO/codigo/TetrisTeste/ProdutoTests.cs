using System;
using Tetris.Model;
using Xunit;

namespace Tetris.Tests
{
    public class ProdutoTests
    {
        private void ResetIDs()
        {
            new Produto("Reset", 0).reset();
        }

        [Fact]
        public void Produto_ValorNegativo_DeveLancarErro()
        {
            ResetIDs();

            Assert.Throws<ArgumentException>(() =>
            {
                new Produto("Erro", -5);
            });
        }

        [Fact]
        public void Produto_CriaCorretamente()
        {
            ResetIDs();

            var produto = new Produto("Teste", 12.5);

            Assert.Equal(1, produto.GetId());
            Assert.Equal(12.5, produto.valor);
        }

        [Fact]
        public void Produto_ToString_DeveConterInformacoes()
        {
            ResetIDs();

            var produto = new Produto("Café", 8.0);

            string texto = produto.ToString();

            Assert.Contains("ID: 1", texto);
            Assert.Contains("Café", texto);
            Assert.Contains("8,00", texto); 
        }

        [Fact]
        public void Produto_IdsIncrementamCorretamente()
        {
            ResetIDs();

            var p1 = new Produto("Um", 1);
            var p2 = new Produto("Dois", 2);
            var p3 = new Produto("Três", 3);

            Assert.Equal(1, p1.GetId());
            Assert.Equal(2, p2.GetId());
            Assert.Equal(3, p3.GetId());
        }
    }
}
