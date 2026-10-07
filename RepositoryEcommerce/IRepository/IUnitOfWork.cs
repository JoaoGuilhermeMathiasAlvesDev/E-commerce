using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IUnitOfWork
    {
        IFuncionarioRepository Funcionario { get; }
        IClienteRepository Cliente { get; }

        ICategoriaProdutoRepository CategoriaProduto { get; }
        ICategoriaRepository Categoria { get; }
        IPedidoRepository Pedido { get; }
        IProdutoRepository Produto { get; }

        IItemPedidoRepository ItemPedido { get; }

        Task BeginTransactionAsync();
        Task<bool> CommitTransactionAsync();
        Task<int> CompleteAsync();
        void Rollback();

        Task SalvarAsync();
    }
}
