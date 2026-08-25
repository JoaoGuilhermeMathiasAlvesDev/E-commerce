using Services.Ecommerce.Models;

namespace Services.Ecommerce.IService
{
    public interface IUsuarioService
    {

       Task<IEnumerable<UsuarioModel>> ObterTodosAsync();
        Task<UsuarioModel> ObterPorIdAsync(Guid id);
        Task AtualizarDadosAsync(Guid id, AtualizarUsuarioModel model);
        Task DesativarUsuarioAsync(Guid id);

    }
}
