using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GasturaApp.Application.Repositories.Implementations;

public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await context.Usuarios.AnyAsync(u => u.Email == email);
    }

    public async Task<Usuario> AdicionarUsuarioAsync(Usuario usuario)
    {
        await context.Usuarios.AddAsync(usuario);
        await context.SaveChangesAsync();
        return usuario;
    }

    public async Task<List<Usuario>> GetAllUsuariosAsync()
    {
        return await context.Usuarios.ToListAsync(); 
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(int id)
    {
        return await context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> UsuarioExisteAsync(int idUsuario)
    {
        return await context.Usuarios.AnyAsync(u => u.Id == idUsuario);
    }
}