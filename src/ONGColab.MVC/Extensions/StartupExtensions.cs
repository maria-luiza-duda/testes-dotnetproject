using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONGColab.Domain;
using ONGColab.Repository;
using ONGColab.Repository.Context;
using ONGColab.Service;
using ONGColab.Service.AutoMapper;

namespace ONGColab.MVC.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ONGColabOnlineDBContext>(opt => opt.UseInMemoryDatabase("ONGColabOnLineDIO"));

            return services;
        }

        public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICausaService, CausaService>();
            services.AddScoped<IHomeInfoService, HomeInfoService>();

            services.AddScoped<IDomainNotificationService, DomainNotificationService>();
            services.AddScoped<IVoluntariadoService, VoluntariadoService>();
            services.AddScoped<IVoluntariadoRepository, VoluntariadoRepository>();

            services.AddScoped<ICausaRepository, CausaRepository>();
            services.AddScoped<IHomeInfoRepository, HomeInfoRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var globalAppSettings = new GloballAppConfig();
            configuration.Bind("ConfiguracoesGeralAplicacao", globalAppSettings);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ONGColabOnLineMappingProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new GloballAppConfig();

            configuration.Bind("ConfiguracoesGeralAplicacao", config);
            services.AddSingleton(config);

            return services;
        }
    }
}
