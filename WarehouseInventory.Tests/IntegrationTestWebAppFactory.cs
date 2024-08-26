using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Testcontainers.MsSql;
using Testcontainers.Redis;
using WarehouseInventory.DB.Entities;

namespace WarehouseInventory.Tests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlBuilder = new MsSqlBuilder()
                                                            .WithCleanUp(true)
                                                            .Build();

        private readonly RedisContainer _redisContainer = new RedisBuilder()
                                                           .WithCleanUp(true)
                                                            .Build();
        public async Task DisposeAsync()
        {
            await _msSqlBuilder.DisposeAsync();
            await _redisContainer.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<WarehouseInventoryContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<WarehouseInventoryContext>((_, options) =>

                    options.UseSqlServer(_msSqlBuilder.GetConnectionString())
                );


                services.Remove(services.SingleOrDefault(service => typeof(IConnectionMultiplexer) == service.ServiceType));

                services.AddSingleton<IConnectionMultiplexer>(sp =>
                {
                    return ConnectionMultiplexer.Connect(_redisContainer.GetConnectionString());
                });



            });
        }

        public async Task InitializeAsync()
        {
            await _msSqlBuilder.StartAsync();
            await _redisContainer.StartAsync();
        }
    }
}
