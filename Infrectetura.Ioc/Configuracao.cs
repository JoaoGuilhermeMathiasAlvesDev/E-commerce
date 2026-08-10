using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using RepositoryEcommerce.Repository;
using Services.Ecommerce.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrectetura.Ioc
{
    public static class Configuracao
    {
        public static void ConexecaoBanco(this IServiceCollection service, IConfiguration iconfiguration)
        {
            var connectionString = iconfiguration.GetConnectionString("DefaultConnection");
            service.AddDbContext<ContextEcommerce>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void IdependenciaRepositorios(this IServiceCollection service)
        {
            var assemblyRepository = typeof(UsuarioRepository).Assembly;

            var repositoryTypes = assemblyRepository.GetTypes()
                .Where(r => r.IsClass && !r.IsAbstract && r.Name.EndsWith("Repository"));

            foreach (var repositoryType in repositoryTypes)
            {
                var interfaceType = repositoryType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{repositoryType.Name}");

                if (interfaceType != null)
                {
                    service.AddScoped(interfaceType, repositoryType);
                }
            }

            service.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            service.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static void IdependenciaServicos(this IServiceCollection service)
        {
            var assemblyService = typeof(UsuarioService).Assembly;
            var serviceTypes = assemblyService.GetTypes()
                .Where(s => s.IsClass && !s.IsAbstract && s.Name.EndsWith("Service"));
            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{serviceType.Name}");
                if (interfaceType != null)
                {
                    service.AddScoped(interfaceType, serviceType);
                }
            }
        }
    }
}   
