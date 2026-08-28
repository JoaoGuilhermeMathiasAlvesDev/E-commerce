using DominioEcommerce.Entitidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryEcommerce.IRepository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;

namespace Services.Ecommerce.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioService(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task AtivarUsuarioAsync(Guid id)
        {
            var obterUsuario = await _userManager.FindByIdAsync(id.ToString());

            if (obterUsuario == null)
                throw new KeyNotFoundException($"Usuario com {id} não encontrado.");

            obterUsuario.Ativar();

            var sucesso = await _userManager.UpdateAsync(obterUsuario);

            if(!sucesso.Succeeded)
                throw new InvalidOperationException(
                                    string.Join("; ", sucesso.Errors.Select(e => e.Description)));
        }

        public async Task AtualizarDadosAsync(Guid id, AtualizarUsuarioModel model)
        {
            var obterUsuario = await _userManager.FindByIdAsync(id.ToString());

            if (obterUsuario == null)
                throw new KeyNotFoundException($"Usuario com {id} não encontrado.");

            obterUsuario.AtualizarDados(model.Nome, model.Sobrenome, model.DataNascimento, model.PhoneNumber);

            var sucesso = await _userManager.UpdateAsync(obterUsuario);

            if (!sucesso.Succeeded)
                throw new InvalidOperationException(
                                    string.Join("; ", sucesso.Errors.Select(e => e.Description)));
        }

        public async Task DesativarUsuarioAsync(Guid id)
        {
            var obterUsuario = await _userManager.FindByIdAsync(id.ToString());

            if (obterUsuario == null)
                throw new KeyNotFoundException($"Usuario com {id} não encontrado.");

            obterUsuario.Desativar();

            var sucesso = await _userManager.UpdateAsync(obterUsuario);

            if (!sucesso.Succeeded)
                throw new InvalidOperationException(
                                    string.Join("; ", sucesso.Errors.Select(e => e.Description)));

        }

        public async Task<UsuarioModel> ObterPorIdAsync(Guid id)
        {
            var obterUsuario = await _userManager.FindByIdAsync(id.ToString());

            if (obterUsuario == null)
                throw new KeyNotFoundException($"Usuario com {id} não encontrado.");

            return new UsuarioModel().ToModel(obterUsuario);
        }

        public async Task<IEnumerable<UsuarioModel>> ObterTodosAsync()
        {
            var usuarios = await _userManager.Users.ToListAsync();

            return usuarios.Select(u => new UsuarioModel().ToModel(u));
        }
    }
}

