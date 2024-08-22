namespace WarehouseInventory.DB.Readonly
{
    public interface ICache
    {
        Task<T> GetAsync<T>(string key, Func<Task<T>> getFromDb = null, TimeSpan? expiry = null);

        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task DeleteAsync(string key);
    }
}
