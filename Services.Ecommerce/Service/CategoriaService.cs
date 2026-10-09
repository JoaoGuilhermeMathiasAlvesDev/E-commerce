using DominioEcommerce.Entitidades;
using RepositoryEcommerce.Repository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoriaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoriaResponseModel> Adicionar(RegistrarCategoriaModel model)
        {
           var novaCategoria = new Categoria(model.Nome, model.Ativo);

            await _unitOfWork.Categoria.Adicionar(novaCategoria);

            await _unitOfWork.SalvarAsync();

            return CategoriaResponseModel.De(novaCategoria);
        }

        public async Task AtivarAsync(Guid id)
        {
            var categoria = await _unitOfWork.Categoria.ObterPorId(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            categoria.Ativar();
            _unitOfWork.Categoria.Atualizar(categoria);
            await _unitOfWork.SalvarAsync();
        }

        public async Task<CategoriaResponseModel> Atualizar(Guid id, AtualizarCategoriaModel model)
        {
           var  categoria = await _unitOfWork.Categoria.ObterPorId(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");


            categoria.AtualizarNome(model.Nome);

            _unitOfWork.Categoria.Atualizar(categoria);
            await _unitOfWork.SalvarAsync();

            return CategoriaResponseModel.De(categoria);
        }

        public async Task DesativarAsync(Guid id)
        {
            var categoria = await _unitOfWork.Categoria.ObterPorId(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            categoria.Desativar();
            _unitOfWork.Categoria.Atualizar(categoria);
            await _unitOfWork.SalvarAsync();
        }

        public async Task<CategoriaResponseModel> ObterPorId(Guid id)
        {
            var categoria =await _unitOfWork.Categoria.ObterPorId(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            return CategoriaResponseModel.De(categoria);
        }

        public async Task<IEnumerable<CategoriaResponseModel>> ObterTodos()
        {
            var categorias = await _unitOfWork.Categoria.ObterTodos();
            return categorias.Select(CategoriaResponseModel.De);
        }
    }
}
