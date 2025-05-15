using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;

namespace GasturaApp.Application.Services.Interfaces;

public interface IUsuarioService
{
    Task<Usuario> CadastrarUsuarioAsync(CreateUsuarioDTO createUsuarioDTO);
    Task<List<ListUsuariosDTO>> GetAllUsuariosAsync();
    Task<Usuario> GetUsuarioByIdAsync(int id);
}
