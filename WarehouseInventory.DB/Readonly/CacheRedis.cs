using StackExchange.Redis;
using System.Text.Json;

namespace WarehouseInventory.DB.Readonly
{
    public class CacheRedis : ICache
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;

        public CacheRedis(IConnectionMultiplexer connectionMultiplexer, int database = -1)
        {
            _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
            _database = _connectionMultiplexer.GetDatabase(database);
        }

        public async Task DeleteAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or whitespace.", nameof(key));

            await _database.KeyDeleteAsync(key);
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> getFromDb = null, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or whitespace.", nameof(key));

            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty && getFromDb != null)
            {
                var dbValue = await getFromDb();
                await this.SetAsync(key, dbValue, null);
                return dbValue;
            }
            else if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or whitespace.", nameof(key));

            var serializedValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serializedValue, expiry);
        }
    }
}
