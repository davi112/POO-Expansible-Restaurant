using System;
using Xunit;
using Moq;
using Tetris.Model;

namespace Tetris.Tests
{
    public class CardapioTests
    {
        private void ResetarIDs()
        {
            new Produto("Reset", 0).reset();
        }

        [Fact]
        public void Cardapio_BuscarProduto_ProdutoInexistente()
        {
            ResetarIDs();
            Cardapio teste = new CardapioCafeteria();

            var exception = Assert.Throws<NullReferenceException>(() => teste.BuscarProduto(999));
            
            Assert.Contains("Object reference not set to an instance", exception.Message);
        }

        [Fact]
        public void Cardapio_BuscarTextoCardapio()
        {
            ResetarIDs();
            Cardapio teste = new CardapioCafeteria();

            String texto = teste.getTextoCardapio();

            Assert.NotNull(texto);
            Assert.Contains("----- Cardápio -----", texto);
            Assert.Contains("Cheesecake de frutas vermelhas", texto);
            Assert.Contains("Café expresso organico", texto);
        }

        [Fact]
        public void Cafeteria_BuscarProduto_ComidaExistente()
        {
            ResetarIDs();
            Cardapio teste = new CardapioCafeteria();

            Produto buscado = teste.BuscarProduto(1);

            Assert.NotNull(buscado);
            Assert.Contains("ID: 1", buscado.ToString());
            Assert.Contains("Cheesecake de frutas vermelhas", buscado.ToString());
            Assert.Contains("15", buscado.ToString());
        }

        [Fact]
        public void Cafeteria_BuscarProduto_BebidaExistente()
        {
            ResetarIDs();
            Cardapio teste = new CardapioCafeteria();

            Produto buscado = teste.BuscarProduto(10);

            Assert.NotNull(buscado);
            Assert.Contains("ID: 10", buscado.ToString());
            Assert.Contains("Café expresso organico", buscado.ToString());
            Assert.Contains("6", buscado.ToString());
        }

        [Fact]
        public void Restaurante_BuscarProduto_ComidaExistente()
        {
            ResetarIDs();
            Cardapio teste = new CardapioRestaurante();

            Produto buscado = teste.BuscarProduto(1);

            Assert.NotNull(buscado);
            Assert.Contains("ID: 1", buscado.ToString());
            Assert.Contains("Moqueca de Palmito", buscado.ToString());
            Assert.Contains("32", buscado.ToString());
        }

        [Fact]
        public void Restaurante_BuscarProduto_BebidaExistente()
        {
            ResetarIDs();
            Cardapio teste = new CardapioRestaurante();

            Produto buscado = teste.BuscarProduto(10);

            Assert.NotNull(buscado);
            Assert.Contains("ID: 10", buscado.ToString());
            Assert.Contains("Cerveja vegana", buscado.ToString());
            Assert.Contains("9", buscado.ToString());
        }
    }
}