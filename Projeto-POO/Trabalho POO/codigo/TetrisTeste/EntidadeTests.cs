using System;
using Tetris.Model;
using Xunit;

namespace Tetris.Tests
{
    public class EntidadeTests
    {
        private void ResetIDs()
        {
            new Produto("Reset", 0).reset();
        }

        [Fact]
        public void Entidade_IdIncrementaCorretamente()
        {
            ResetIDs();

            var p1 = new Produto("A", 10);
            var p2 = new Produto("B", 20);

            Assert.Equal(1, p1.GetId());
            Assert.Equal(2, p2.GetId());
        }

        [Fact]
        public void Entidade_IdEhIndependentePorTipo()
        {
            ResetIDs();

            var p1 = new Produto("X", 5);
            var e1 = new EntidadeFake(); 

            Assert.Equal(1, p1.GetId()); 
            Assert.Equal(1, e1.GetId()); 
        }

        [Fact]
        public void Entidade_Reset_ZeraContadores()
        {
            ResetIDs();

            var p1 = new Produto("A", 10);
            var p2 = new Produto("B", 10);

            Assert.Equal(1, p1.GetId());
            Assert.Equal(2, p2.GetId());

            p1.reset();

            var p3 = new Produto("C", 10);

            Assert.Equal(1, p3.GetId()); 
        }

        
        private class EntidadeFake : Entidade
        {
        }
    }
}
