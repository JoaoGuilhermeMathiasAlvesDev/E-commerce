using RepositoryEcommerce.IRepository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;

namespace Services.Ecommerce.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task AtivarUsuarioAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DesativarUsuarioAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioModel> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioModel>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}

