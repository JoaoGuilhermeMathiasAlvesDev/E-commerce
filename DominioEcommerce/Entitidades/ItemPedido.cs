using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class ItemPedido : EntityBase.EntityBase
    {
        public Guid PedidoId { get; private set; }

        public Guid ProdutoId { get; private set; }
        public string NomeProdutoNoMomentodaCompra { get; private set; } = string.Empty;

        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        public decimal TotalItem => PrecoUnitario * Quantidade;

        protected ItemPedido()
        {

        }

        public ItemPedido(Guid pedidoId, Guid produtoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            CriarItemPedido(pedidoId, produtoId, nomeProduto, quantidade, precoUnitario);
        }

        private void CriarItemPedido(Guid pedidoId, Guid produtoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            if (produtoId == Guid.Empty || pedidoId == Guid.Empty)
                throw new DominioException.DominioException("O ID do produto e pedido é obrigatório.",
                                                                new List<string> { "O ID do produto e pedido é obrigatório." });
            if (string.IsNullOrWhiteSpace(nomeProduto))
                throw new DominioException.DominioException("O nome do produto não pode ser vazio.",
                                                                        new List<string> { "O nome do produto não pode ser vazio." });

            if (quantidade <= 0) throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");
            if (precoUnitario < 0) throw new ArgumentOutOfRangeException(nameof(precoUnitario), "O preço unitario não pode ser negativo.");

            PedidoId = pedidoId;
            ProdutoId = produtoId;
            NomeProdutoNoMomentodaCompra = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AtualizarQuantidade(int novaQuantidade)
        {
            if (novaQuantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(novaQuantidade), "A quantidade deve ser maior que zero.");

            Quantidade = novaQuantidade;
        }
    }
}
