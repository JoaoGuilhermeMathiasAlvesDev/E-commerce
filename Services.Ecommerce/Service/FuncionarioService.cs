using DominioEcommerce.DominioException;
using DominioEcommerce.Entitidades;
using DominioEcommerce.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RepositoryEcommerce.IRepository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Service
{
    public class FuncionarioService : IFuncionarioSerivice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FuncionarioService(IUnitOfWork unitOfWork, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FuncionarioResponseModel> AdicionarAsync(RegistrarFuncionarioModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            var obterOsEmails = await _unitOfWork.Funcionario.ObterTodosEmails();
            var verificarEmail = Usuario.VerificarSerExisteEmail(model.Email, obterOsEmails);

            if (verificarEmail)
                throw new DominioException("E-mail já registrado.",
                    new List<string> { "E-mail já registrado." });

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var ultimaMatricula = await _unitOfWork.Funcionario.ObterUltimaMatricula();
                var matricula = Funcionario.CriarMatricula(model.Matricula, ultimaMatricula);

                var endreco = new Endereco(model.Endereco.Logradouro, model.Endereco.Numero, model.Endereco.Complemento,
                    model.Endereco.Bairro, model.Endereco.Cidade, model.Endereco.Estado, model.Endereco.Cep);

                var funcionarioNovo = new Funcionario(
                    model.Nome, model.SobreNome, model.DataNascimento,model.Senha,
                    model.Email, model.PhoneNumber, matricula, endreco);

                var resultado = await _userManager.CreateAsync(funcionarioNovo, model.Senha);

                if (!resultado.Succeeded)
                {
                    var erros = resultado.Errors.Select(e => e.Description).ToList();
                    throw new DominioException("Erro ao cadastrar funcionário.", erros);
                }

                if (!await _unitOfWork.CommitTransactionAsync())
                    throw new DominioException("Erro ao cadastrar funcionário.",
                        new List<string> { "Não foi possível confirmar a operação." });

                return FuncionarioResponseModel.De(funcionarioNovo);
            }
            catch
            {
                 _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task Atualizar(FuncionarioResponseModel funcionario)
        {
           var obterUsuario = await _unitOfWork.Funcionario.ObterPorId(funcionario.Id);
            if (obterUsuario == null)
                throw new DominioException("Funcionário não encontrado.",
                    new List<string> { "Funcionário não encontrado." });


            obterUsuario.AtualizarDados(funcionario.Nome, funcionario.SobreNome,funcionario.DataNascimento,funcionario.PhoneNumber);
           var resultado = await _userManager.UpdateAsync(obterUsuario);

            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors.Select(e => e.Description).ToList();
                throw new DominioException("Erro ao cadastrar funcionário.", erros);
            }
        }


        public async Task<IEnumerable<FuncionarioResponseModel>> ListarAsync()
        {
            var obterTodosFuncionarios = await _unitOfWork.Funcionario.Listar();

            return obterTodosFuncionarios.Select(f => FuncionarioResponseModel.De(f));
        }

        public async Task<FuncionarioResponseModel> ObterPorIdAsync(Guid id)
        {
            var funcionario = await _unitOfWork.Funcionario.ObterPorId(id);

            if (funcionario == null)
                throw new DominioException("Funcionário não encontrado.",
                    new List<string> { "Funcionário não encontrado." });

            return FuncionarioResponseModel.De(funcionario);
        }


        public async Task<int> SalvarAsync()
        {
            return await _unitOfWork.CompleteAsync();
        }
    }
}
