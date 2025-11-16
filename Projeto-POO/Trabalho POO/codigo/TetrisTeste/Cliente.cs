using Xunit;
using Tetris.Model;

namespace Tetris.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void Construtor_DeveInicializarNomeCorretamente()
        {
            // Arrange
            var cliente = new Cliente("Maria");

            // Act
            string nome = cliente.GetNome();

            // Assert
            Assert.Equal("Maria", nome);
        }

        [Fact]
        public void GetNome_DeveRetornarNomeCorreto()
        {
            // Arrange
            var cliente = new Cliente("João");

            // Act
            var nome = cliente.GetNome();

            // Assert
            Assert.Equal("João", nome);
        }

        [Fact]
        public void ToString_DeveConterONomeDoCliente()
        {
            // Arrange
            var cliente = new Cliente("Carlos");

            // Act
            string texto = cliente.ToString();

            // Assert
            Assert.Contains("Carlos", texto);
        }
    }
}
