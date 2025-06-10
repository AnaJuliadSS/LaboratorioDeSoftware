using GasturaApp.Application.Helpers.Mapper;
using GasturaApp.Application.Repositories.Interfaces;
using GasturaApp.Application.Services.Interfaces;
using GasturaApp.Core.DTOs;
using GasturaApp.Core.Entities;
using GasturaApp.Infrastructure.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace GasturaApp.Application.Services.Implementations;

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    public async Task<Usuario> ValidarEAdicionarUsuarioAsync(CreateUsuarioDTO createUsuarioDTO)
    {
        #region validações
        if (await usuarioRepository.ExisteEmailAsync(createUsuarioDTO.Email))
            throw new EmailJaCadastradoException();

        if (ContemSequencia(createUsuarioDTO.Senha))
            throw new SenhaNaoDeveConterSequenciasException();
        #endregion

        createUsuarioDTO.Senha = GerarSenhaHash(createUsuarioDTO.Senha);

        Usuario usuario = Mapper.Map<Usuario>(createUsuarioDTO);        

        return await usuarioRepository.AdicionarUsuarioAsync(usuario);
    }

    private static string GerarSenhaHash(string senha)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(hashBytes);
    }

    public async Task<List<ListUsuariosDTO>> GetAllUsuariosAsync()
    {
        var usuarios = await usuarioRepository.GetAllUsuariosAsync();

        return usuarios.Select(u => Mapper.Map<ListUsuariosDTO>(u)).ToList();
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        return await usuarioRepository.GetUsuarioByIdAsync(id) ?? throw new EntidadeNaoEncontradaException($"Usuário com ID {id} não encontrado.");
    }

    private static bool ContemSequencia(string senha)
    {
        var sequencias = new List<string>
        {
            "0123456789",
            "abcdefghijklmnopqrstuvwxyz",
            "abcdefghijklmnopqrstuvwxyz".ToUpper(),
            "qwertyuiopasdfghjklzxcvbnm"
        };

        foreach (var seq in sequencias)
        {
            for (int i = 0; i <= seq.Length - 3; i++)
            {
                var sub = seq.Substring(i, 3);
                if (senha.Contains(sub) || senha.Contains(new string(sub.Reverse().ToArray())))
                    return true;
            }
        }

        return false;
    }
}
