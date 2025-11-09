using System;
using Xunit;
using Moq;
using Tetris.Model;

namespace Tetris.Tests
{
    public class CardapioTests
    {

        #region Cardapio Tests
        // buscar produto
        [Fact]
        public void Cardapio_BuscarProduto_ProdutoInexistente()
        {
            // Given
            Cardapio teste = new CardapioCafeteria();

            // When
            var exception = Assert.Throws<NullReferenceException>(() => teste.BuscarProduto(999));

            // Then
            Assert.Contains("Object reference not set to an instance", exception.Message);

            teste.reset();
        }


        [Fact]
        public void Cardapio_BuscarTextoCardapio()
        {
            // Given
            Cardapio teste = new CardapioCafeteria();

            // When
            String texto = teste.getTextoCardapio();

            // Then
            Assert.NotNull(texto);
            Assert.Contains("----- Cardápio -----", texto);
            Assert.Contains("Cheesecake de frutas vermelhas", texto);
            Assert.Contains("Café expresso organico", texto);

            teste.reset();
        }
        #endregion


        #region CardapioCafeteria tests 
        [Fact]
        public void Cafeteria_BuscarProduto_ComidaExistente()
        {
            Cardapio teste = new CardapioCafeteria();

            // When
            Produto buscado = teste.BuscarProduto(1);

            // Then
            Assert.NotNull(buscado);
            // Assert.Equal("ID: 1 | Descrição: Cheesecake de frutas vermelhas | Valor: R$ 15,00", buscado.ToString());
            Assert.Contains("ID: 1", buscado.ToString());
            Assert.Contains("Cheesecake de frutas vermelhas", buscado.ToString());
            Assert.Contains("15", buscado.ToString());

            teste.reset();
        }

        [Fact]
        public void Cafeteria_BuscarProduto_BebidaExistente()
        {
            Cardapio teste = new CardapioCafeteria();

            // When
            Produto buscado = teste.BuscarProduto(10);

            // Then
            Assert.NotNull(buscado);
            // Assert.Equal("ID: 10 | Descrição: Café expresso organico | Valor: R$ 6,00", buscado.ToString());
            Assert.Contains("ID: 10", buscado.ToString());
            Assert.Contains("Café expresso organico", buscado.ToString());
            Assert.Contains("6", buscado.ToString());

            teste.reset();
        }
        #endregion


        #region CardapioRestaurante tests
        [Fact]
        public void Restaurante_BuscarProduto_ComidaExistente()
        {
            Cardapio teste = new CardapioRestaurante();

            // When
            Produto buscado = teste.BuscarProduto(1);

            // Then
            Assert.NotNull(buscado);
            // Assert.Equal("ID: 1 | Descrição: Cheesecake de frutas vermelhas | Valor: R$ 15,00", buscado.ToString());
            Assert.Contains("ID: 1", buscado.ToString());
            Assert.Contains("Moqueca de Palmito", buscado.ToString());
            Assert.Contains("32", buscado.ToString());

            teste.reset();
        }

        [Fact]
        public void Restaurante_BuscarProduto_BebidaExistente()
        {
            Cardapio teste = new CardapioRestaurante();

            // When
            Produto buscado = teste.BuscarProduto(10);

            // Then
            Assert.NotNull(buscado);
            // Assert.Equal("ID: 10 | Descrição: Café expresso organico | Valor: R$ 6,00", buscado.ToString());
            Assert.Contains("ID: 10", buscado.ToString());
            Assert.Contains("Cerveja vegana", buscado.ToString());
            Assert.Contains("9", buscado.ToString());

            teste.reset();
        }
        #endregion
    }
}
