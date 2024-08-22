using dotenv.net;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WarehouseInventory.Application.Configuration;
using WarehouseInventory.Core.Constants;
using WarehouseInventory.DB.Entities;
using WarehouseInventory.DB.Readonly;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WarehouseInventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddMediatrDependencies();
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjectionExtension).Assembly));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")!, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddScoped<ICache, CacheRedis>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
