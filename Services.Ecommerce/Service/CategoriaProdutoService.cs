using DominioEcommerce.DominioException;
using DominioEcommerce.Entitidades;
using RepositoryEcommerce.IRepository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Services.Ecommerce.IService.ICategoriaProdutoServices;

namespace Services.Ecommerce.Service
{
    public class CategoriaProdutoService : ICategoriaProdutoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaProdutoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProdutoResponseModel> AssociarAsync(Guid produtoId, Guid categoriaId)
        {
            var categoriaProduto = new CategoriaProduto(produtoId, categoriaId);


            await _unitOfWork.CategoriaProduto.Adicionar(categoriaProduto);
            await _unitOfWork.SalvarAsync();

            var produto = await _unitOfWork.Produto.ObterPorId(produtoId);

            return ProdutoResponseModel.De(produto);
        }

        public async Task<ProdutoResponseModel> DefinirCategoriasAsync(Guid produtoId, DefinirCategoriasDoProdutoModel model)
        {
            var produto = await _unitOfWork.Produto.ObterPorId(produtoId)
                   ?? throw new Exception("Produto não encontrado.");


            var ids = (model.CategoriaIds ?? new()).Distinct().ToList();

            var categorias = await _unitOfWork.Categoria.ObterPorIdsAsync(ids);

            var naoEncontradas = ids.Except(categorias.Select(c => c.Id)).ToList();
            if (naoEncontradas.Count > 0)
                throw new Exception(
                    $"Categorias não encontradas: {string.Join(", ", naoEncontradas)}.");

            var inativas = categorias.Where(c => !c.Ativo).Select(c => c.Nome).ToList();
            if (inativas.Count > 0)
                throw new DominioException(
                    "Não é possível associar categorias inativas.",
                    inativas.Select(n => $"A categoria '{n}' está inativa.").ToList());



            foreach (var categoria in categorias)
            {
                if (!categoria.Ativo)
                    throw new DominioException(
                        "Não é possível associar categorias inativas.",
                        new List<string> { $"A categoria '{categoria.Nome}' está inativa." });

                produto.AdicionarCategoria(categoria.Id);
            }
           

            await _unitOfWork.SalvarAsync();

            return ProdutoResponseModel.De(produto);

        }

        public async Task<ProdutoResponseModel> DesassociarAsync(Guid produtoId, Guid categoriaId)
        {
            var produto = await _unitOfWork.CategoriaProduto.ObterTodos();

            var categoriaProduto = produto.FirstOrDefault(p => p.ProdutoId == produtoId && p.CategoriaId == categoriaId);

            if (categoriaProduto == null)
                throw new Exception("Associação não encontrada.");

            _unitOfWork.CategoriaProduto.Remover(categoriaProduto.Id);

            await _unitOfWork.SalvarAsync();

            return ProdutoResponseModel.De(await _unitOfWork.Produto.ObterPorId(produtoId));
        }

        public async Task<IEnumerable<CategoriaResponseModel>> ObterCategoriasDoProdutoAsync(Guid produtoId)
        {
            var categorias = await _unitOfWork.CategoriaProduto.ObterTodos();

             var categoriasDoProduto = categorias.Where(cp => cp.ProdutoId == produtoId)
                .Select(cp => cp.CategoriaId)
                .ToList();

            var categoriasExistentes = await _unitOfWork.Categoria.ObterPorIdsAsync(categoriasDoProduto);

            return categoriasExistentes.Select(c => CategoriaResponseModel.De(c)).ToList();
        }

        public async Task<IEnumerable<ProdutoResponseModel>> ObterProdutosDaCategoriaAsync(Guid categoriaId)
        {
            var categoriaProdutos = await _unitOfWork.CategoriaProduto.ObterTodos();
            
           var produtosIds = categoriaProdutos.Where(cp => cp.CategoriaId == categoriaId)
                .Select(cp => cp.ProdutoId)
                .ToList();

            var produtos = new List<Produto>();

            foreach (var p in produtosIds)
            {
              produtos.Add(await _unitOfWork.Produto.ObterPorId(p));
            }

            return produtos.Select(p => ProdutoResponseModel.De(p)).ToList();
        }
    }
}
