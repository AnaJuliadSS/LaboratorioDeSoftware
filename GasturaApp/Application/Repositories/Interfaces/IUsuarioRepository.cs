using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<bool> ExisteEmailAsync(string email);
    Task<Usuario> AdicionarUsuarioAsync(Usuario usuario);
    Task<List<Usuario>> GetAllUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(int id);
    Task<bool> UsuarioExisteAsync(int idUsuario);
}
