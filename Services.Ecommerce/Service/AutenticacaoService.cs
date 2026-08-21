using DominioEcommerce.DominioException;
using DominioEcommerce.Entitidades;
using DominioEcommerce.Enum;
using Microsoft.AspNetCore.Identity;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Ecommerce.Service
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;

        public AutenticacaoService(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(Login model)
        {
            var usuario = await _userManager.FindByEmailAsync(model.Email);
            if (usuario == null || !usuario.Ativo)
            {
                throw new DominioException("Usuário inativo ou não encontrado.",
                    new List<string> { "E-mail ou senha incorretos." });
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, model.Senha, lockoutOnFailure: true);

            if (!resultado.Succeeded)
            {
                throw new DominioException("Tentativa de login inválida.",
                    new List<string> { "E-mail ou senha incorretos." });
            }

            return await _tokenService.GerarToken(usuario);
        }

        public async Task<string> RegistrarClienteAsync(RegistrarClienteModel model)
        {
            var cliente = new Cliente(
                model.Nome,
                model.SobreNome,
                model.DataNascimento,
                model.Email,
                model.PhoneNumber,
                model.Senha,
                model.Endereco
            );

            var resultado = await _userManager.CreateAsync(cliente, model.Senha);

            if (!resultado.Succeeded)
            {
                var erros = new List<string>();
                foreach (var erro in resultado.Errors)
                {
                    erros.Add(erro.Description);
                }
                throw new DominioException("Erro ao registrar cliente.", erros);
            }

            await _userManager.AddToRoleAsync(cliente, RoleUsuario.Cliente.ToString());

            return await _tokenService.GerarToken(cliente);
        }

        public async Task<string> RegistrarFuncionarioAsync(RegistrarFuncionarioModel model)
        {

            int valorInt = model.Role;

            if (!System.Enum.IsDefined(typeof(RoleUsuario), valorInt))
            {
                throw new DominioException("Role inválida.",
                    new List<string> { "Role inválida." });
            }

            RoleUsuario roleEnum = (RoleUsuario)valorInt;

            var funcionario = new Funcionario(
                 model.Nome,
                 model.SobreNome,
                 model.DataNascimento,
                 model.Email,
                 model.PhoneNumber,
                 model.Senha,
                 model.Matricula, 
                 model.Endereco,
                 roleEnum         
             );

            var resultado = await _userManager.CreateAsync(funcionario, model.Senha);

            if (!resultado.Succeeded)
            {
                var erros = new List<string>();
                foreach (var erro in resultado.Errors)
                {
                    erros.Add(erro.Description);
                }
                throw new DominioException("Erro ao registrar funcionário.", erros);
            }

            await _userManager.AddToRoleAsync(funcionario, model.Role.ToString());

            return await _tokenService.GerarToken(funcionario);
        }
    }
}