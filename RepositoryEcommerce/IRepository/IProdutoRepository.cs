using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<List<Produto>> ListaAtivos();
        Task<bool> ExisteProduto(string nome);
    }
}
