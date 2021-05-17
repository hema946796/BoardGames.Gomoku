using BoardGames.Gomoku.Business;
using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGames.Gomoku.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ApiSettings ApiSettings { get; private set; }
        public static CosmosDBSettings CosmosDBSettings { get; private set; }

        public static IServiceCollection AddApiDependencies(this IServiceCollection services)
        {
            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<IGameService, GameService>();
            services.AddCosmosRepository(options =>
            {
                options.CosmosConnectionString = CosmosDBSettings.ConnectionString;
                options.DatabaseId = CosmosDBSettings.DatabaseName;
                options.ContainerId = CosmosDBSettings.ContainerName;
            });
            return services;
        }

        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("apiSettings"));
            ApiSettings = configuration.GetSection("apiSettings").Get<ApiSettings>();

            services.Configure<ApiSettings>(configuration.GetSection("CosmosDb"));
            CosmosDBSettings = configuration.GetSection("CosmosDb").Get<CosmosDBSettings>();

            return services;
        }
    }
}
