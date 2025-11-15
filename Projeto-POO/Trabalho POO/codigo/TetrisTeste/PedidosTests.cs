using System;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;
using Moq;
using Tetris.Model;

namespace Tetris.Model
{
    public class Pedido : Entidade
    {
        private List<Produto> Itens;
        private const double TX_SERVICO = 0.10;

        // Propriedade pública para status do pedido
        public bool StatusPedidoAberto { get; private set; } = false;

        // Construtor
        public Pedido()
        {
            Itens = new List<Produto>();
        }

        // Retornar lista de itens
        public List<Produto> GetPedido()
        {
            return Itens;
        }

        // Adicionar item ao pedido com validação
        public void AdicionarItem(Produto novo)
        {
            if (novo == null || string.IsNullOrWhiteSpace(novo.Nome) || novo.valor < 0 || novo.Quantidade <= 0)
            {
                throw new ArgumentException("Produto inválido.");
            }

            Itens.Add(novo);
        }

        // Remover item do pedido
        public void RemoverItem(Produto item)
        {
            if (item == null)
                throw new ArgumentException("Item inválido para remoção.");

            Itens.Remove(item);
        }

        // Abrir pedido
        public void GerarPedido(Produto produto)
        {
            StatusPedidoAberto = true;

            if (produto != null)
                AdicionarItem(produto);
        }

        // Fechar pedido
        public void FecharPedido()
        {
            StatusPedidoAberto = false;
        }

        // Calcular valor total com taxa
        public double CalcularValorTotal()
        {
            return Itens.Sum(x => x.valor * x.Quantidade * (1 + TX_SERVICO));
        }

        // Calcular divisão do valor
        public double CalcularDivisaoValor(int quantidadeDivisoes)
        {
            if (quantidadeDivisoes <= 0)
                throw new ArgumentException("Quantidade de divisões inválida.");

            return CalcularValorTotal() / quantidadeDivisoes;
        }

        // Representação em string do pedido
        public override string ToString()
        {
            string tmp = "";
            foreach (Produto item in Itens)
            {
                tmp += item.ToString() + "\n";
            }
            return tmp;
        }
    }
}
