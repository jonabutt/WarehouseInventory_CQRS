using Microsoft.Extensions.DependencyInjection;

namespace WarehouseInventory.Application.Configuration
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddMediatrDependencies(this IServiceCollection services)
        {
            return services;
        }
    }
}
