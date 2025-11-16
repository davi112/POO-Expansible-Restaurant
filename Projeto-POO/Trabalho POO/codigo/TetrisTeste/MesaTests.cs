using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Model;

namespace Tetris.Tests
{
    public class MesaTests
    {
        [Fact]
        public void Construtor_DeveIniciarMesaDesocupada()
        {
            // Arrange
            var mesa = new Mesa(4);

            // Act
            bool disponibilidade = mesa.VerificarDisponibilidade(1);

            // Assert
            Assert.True(disponibilidade);
        }

        [Fact]
        public void OcuparMesa_DeveRetornarTrueEDeixarMesaOcupada()
        {
            // Arrange
            var mesa = new Mesa(4);

            // Act
            bool resultado = mesa.OcuparMesa();
            bool disponibilidade = mesa.VerificarDisponibilidade(1);

            // Assert
            Assert.True(resultado);
            Assert.False(disponibilidade);
        }

        [Fact]
        public void LiberarMesa_DeveRetornarFalseEDeixarMesaDisponivel()
        {
            // Arrange
            var mesa = new Mesa(4);
            mesa.OcuparMesa();

            // Act
            bool resultado = mesa.LiberarMesa();
            bool disponibilidade = mesa.VerificarDisponibilidade(1);

            // Assert
            Assert.False(resultado);
            Assert.True(disponibilidade);
        }

        [Fact]
        public void VerificarDisponibilidade_DeveRetornarFalse_SeCapacidadeInsuficiente()
        {
            // Arrange
            var mesa = new Mesa(4);

            // Act
            bool disponibilidade = mesa.VerificarDisponibilidade(6);

            // Assert
            Assert.False(disponibilidade);
        }

        [Fact]
        public void VerificarDisponibilidade_DeveRetornarFalse_SeMesaOcupada()
        {
            // Arrange
            var mesa = new Mesa(4);
            mesa.OcuparMesa();

            // Act
            bool disponibilidade = mesa.VerificarDisponibilidade(2);

            // Assert
            Assert.False(disponibilidade);
        }

        [Fact]
        public void ToString_DeveConterInformacoesDaMesa()
        {
            // Arrange
            var mesa = new Mesa(4);

            // Act
            var texto = mesa.ToString();

            // Assert
            Assert.Contains("capacidade 4", texto);
            Assert.Contains("ocupada: False", texto);
        }
    }
}
