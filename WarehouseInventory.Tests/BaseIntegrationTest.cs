using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;
using WarehouseInventory.DB.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WarehouseInventory.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly WarehouseInventoryContext DbContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

            DbContext = _scope.ServiceProvider.GetRequiredService<WarehouseInventoryContext>();
            DbContext.Database.EnsureCreated();
        }
    }
}
