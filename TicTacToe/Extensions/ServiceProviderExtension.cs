using TicTacToe.BLL;
using TicTacToe.DL;

namespace TicTacToe.Extensions
{
    public static class ServiceProviderExtension
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITicTacToeService, TicTacToeService>();
            services.AddScoped<ITicTacToeData, TicTacToeData>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
